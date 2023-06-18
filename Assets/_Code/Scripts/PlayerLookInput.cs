using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookInput : MonoBehaviour
{
    private Camera _playerCamera;
    private float _cameraRotation;

    private Vector3 _cameraOrigin = new(0, 0.8f, 0);

    [SerializeField] [Range(1, 5f)] private float _mouseSensitivity = 1.5f;
    private const int SENSITIVITY_DIVIDER = 80;
    [SerializeField] private bool _allowLookInput = true;

    [SerializeField] private int _maxVerticalRotation = 45;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    /// <summary>
    /// Resets camera position to the start position
    /// </summary>
    public void ResetCamPos()
    {
        _playerCamera.transform.SetLocalPositionAndRotation(_cameraOrigin, Quaternion.identity);
    }

    /// <summary>
    /// Rotates the player body and camera based on mouse input
    /// </summary>
    /// <param name="context">Vector2 from mouse input</param>
    public void OnLook(InputAction.CallbackContext context)
    {
        if (!_allowLookInput) return;

        Vector2 input = context.ReadValue<Vector2>();
        _cameraRotation -= input.y * _mouseSensitivity / SENSITIVITY_DIVIDER;

        _cameraRotation = Mathf.Clamp(_cameraRotation, -_maxVerticalRotation, _maxVerticalRotation);

        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);

        transform.Rotate(input.x * _mouseSensitivity * Vector3.up / SENSITIVITY_DIVIDER);
    }
}
