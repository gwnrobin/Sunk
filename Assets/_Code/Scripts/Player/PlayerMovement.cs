using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    private bool _isMoving = false;
    private bool _isSprinting = false;
    private bool IsSprinting
    {
        set { _isSprinting = value;

            if (_isSprinting)
                _sprintMultiplier = 1.7f;
            else
                _sprintMultiplier = 1.0f;
        }
    }
    private float _sprintMultiplier = 1f;
    private bool _isJumpPressed;

    [SerializeField] private float _jumpForce = 3.5f;

    private const int GROUNDED_GRAVITY = -5;

    private Vector3 _movementDirection;
    private Vector3 _currentMovement;

    private Vector3 _velocity;
    public bool IsGrounded => _characterController.isGrounded;

    [SerializeField] private float _deceleration = 1f;
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _acceleration = .05f;

    private Camera _playerCamera;

    [SerializeField] private bool _freeze = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        HandleJump();

        _currentMovement = ConvertMoveDirection();

        if (!_freeze)
        {
            _characterController.Move(_currentMovement * Time.fixedDeltaTime);
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Applies input from jump key
    /// </summary>
    /// <param name="context">Value jump key</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    /// <summary>
    /// Apply movement from the keys
    /// </summary>
    /// <param name="context">Vector2 that contains input</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.magnitude > 0)
            _isMoving = true;
        else
            _isMoving = false;

        _movementDirection = new Vector3(input.x, 0, input.y);
    }

    /// <summary>
    /// Apply input from sprint key
    /// </summary>
    /// <param name="context"></param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        IsSprinting = context.ReadValueAsButton();
    }

    /// <summary>
    /// Returns movement vector based on if the player is inside the water
    /// </summary>
    private Vector3 ConvertMoveDirection()
    {
        if (_movementDirection.magnitude <= 0)
        {
            return Mathf.Lerp(_currentMovement.magnitude, 0, _deceleration) * _currentMovement.normalized;
        }
        Vector3 movement = _movementSpeed * _sprintMultiplier * (_movementDirection.x * transform.right + _movementDirection.z * transform.forward);
        float wantedSpeed = Mathf.Lerp(_currentMovement.magnitude, movement.magnitude, _acceleration);
        return wantedSpeed * movement.normalized;
    }

    private void HandleJump()
    {
        if (!IsGrounded || !_isJumpPressed) return;

        _isJumpPressed = false;

        _velocity.y = _jumpForce;
    }

    private void ApplyGravity()
    {
        if (IsGrounded)
            _velocity.y = GROUNDED_GRAVITY;
        else
            _velocity += Physics.gravity * Time.deltaTime;
    }
}
