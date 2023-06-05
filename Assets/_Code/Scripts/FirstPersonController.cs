using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class FirstPersonController : MonoBehaviour
{
    private bool _isMoving = false;
    public bool IsMoving => _isMoving;

    private float _sprintMultiplier = 2f;
    [SerializeField] private float _deceleration = 1f;
    [SerializeField] private float _acceleration = .05f;
    [SerializeField] private float _movementSpeed = 5f;   // Speed of the movement
    [SerializeField] private float _mouseSensitivity = 100f;   // Mouse sensitivity for looking around

    private const float _sensitivityMultiplier = 0.01f;

    private float _verticalRotation = 0f;
    [SerializeField] private float _verticalRange = 60f;

    private CharacterController _characterController;

    private Vector3 _rawMovement;
    private Vector3 _currentMovement;

    [SerializeField] private Camera _playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement
        float forwardSpeed = Input.GetAxis("Vertical") * _movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * _movementSpeed;

        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;

        _characterController.SimpleMove(speed);

        // Player rotation
        float rotX = Input.GetAxis("Mouse X") * _mouseSensitivity * _sensitivityMultiplier;
        transform.Rotate(0, rotX, 0);

        _verticalRotation -= Input.GetAxis("Mouse Y") * _mouseSensitivity * _sensitivityMultiplier;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRange, _verticalRange);
        Camera.main.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);

        // Unlock cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        _currentMovement = ConvertMoveDirection();
    }
    /// <summary>
    /// Apply movement from the keys
    /// </summary>
    /// <param name="context">Vector2 that contains input</param>
    public void OnMovementPressed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.magnitude > 0)
            _isMoving = true;
        else
            _isMoving = false;

        _rawMovement = new Vector3(input.x, 0, input.y);
    }
    /// <summary>
    /// Returns movement vector based on if the player is inside the water
    /// </summary>
    private Vector3 ConvertMoveDirection()
    {
        if (_rawMovement.magnitude <= 0)
        {
            return Mathf.Lerp(_rawMovement.magnitude, 0, _deceleration) * transform.forward;
        }
        Vector3 movement = _movementSpeed * _sprintMultiplier * (_rawMovement.x * transform.right + _rawMovement.z * transform.forward);
        float wantedSpeed = Mathf.Lerp(_rawMovement.magnitude, movement.magnitude, _acceleration);
        return wantedSpeed * movement.normalized;
    }

    /// <summary>
    /// Rotates the player body and camera based on mouse input
    /// </summary>
    /// <param name="context">Vector2 from mouse input</param>
    public void OnLook(CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _verticalRotation -= input.y * _mouseSensitivity * _sensitivityMultiplier;

        _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRange, _verticalRange);

        _playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);

        transform.Rotate(_mouseSensitivity * _sensitivityMultiplier * input.x * Vector3.up);
    }
}