using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded => _characterController.isGrounded;

    [SerializeField] private float _floorDrag = .2f;
    [SerializeField] private float _airDrag = .05f;

    [SerializeField] private float _maxMovementSpeed = 2;
    [SerializeField] private float _jumpForce = 3.5f;

    [SerializeField] private bool _freeze = false;

    private CharacterController _characterController;

    private bool _isMoving = false;
    private bool _isSprinting = false;

    private bool _isJumpPressed;

    private float _drag = .2f;

    private Vector2 _input = Vector2.zero;
    private Vector3 _velocity = Vector3.zero;
    private float _velocityY = 0;

    public bool IsSprinting
    {
        set
        {
            if(_isSprinting != value)
            {
                _isSprinting = value;

                if (_isSprinting)
                    _maxMovementSpeed *= 1.7f;
                else
                    _maxMovementSpeed /= 1.7f;
            }
        }
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        HandleJump();
        HandleMovement();
        ApplyGravity();

        _characterController.Move((_velocity + _velocityY * transform.up) * Time.fixedDeltaTime);

        _velocityY = _characterController.velocity.y;
        _velocity = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z);
    }

    private void HandleMovement()
    {
        Vector3 movement = transform.TransformDirection(new Vector3(_input.x, 0, _input.y));

        if (_velocity.magnitude > _maxMovementSpeed)
        {
            _velocity -= _velocity.normalized * (_velocity.magnitude - _maxMovementSpeed);
        }

        if (!IsGrounded)
        {
            _drag = _airDrag;
        }
        else
        {
            _drag = _floorDrag;
        }

        _velocity -= _velocity * _drag;

        _velocity += movement;
    }

    private void HandleJump()
    {
        if (!IsGrounded || !_isJumpPressed) 
            return;

        _isJumpPressed = false;

        _velocityY = _jumpForce;
    }

    private void ApplyGravity()
    {
        _velocityY += Physics.gravity.y * Time.deltaTime;
    }

    #region InputFunctions
    public void OnJump(CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    public void OnMove(CallbackContext context)
    {
        if (_freeze)
            return;

        _input = context.ReadValue<Vector2>();
        _isMoving = _input.magnitude > 0;
    }

    public void OnSprint(CallbackContext context)
    {
        IsSprinting = context.ReadValueAsButton();
    }
    #endregion
}
