using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactionText;

    [SerializeField] private int _interactRange = 3;

    private Transform _playerCamera;

    private RaycastHit _hit;

    private Interactable _currentInteractable;

    private void Start()
    {
        _playerCamera = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        UpdateRaycast();
    }

    /// <summary>
    /// Updates the hit object which is front of the players camera
    /// </summary>
    private void UpdateRaycast()
    {
        _interactionText.text = "";
        _hit = default;

        if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out _hit, _interactRange) && (_currentInteractable == null || _hit.collider.gameObject.GetInstanceID() == _currentInteractable.gameObject.GetInstanceID()))
        {
            if (_hit.collider.TryGetComponent(out _currentInteractable))
            {
                _interactionText.text = _currentInteractable.InteractText;
            }
        }
    }

    /// <summary>
    /// Function that calls the interact event on the object the player is looking at
    /// </summary>
    public void Interact(CallbackContext context)
    {
        if (!context.performed || _hit.collider == null) return;

        _hit.collider.GetComponent<IInteract>()?.OnInteract();
    }
}