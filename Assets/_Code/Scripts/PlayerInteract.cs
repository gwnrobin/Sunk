using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] public TMP_Text _interactionText;
    [SerializeField] private int _interactRange = 3;

    public float interactCD = 0;

    private Transform _playerCamera;
    private RaycastHit _hit;
    public Interactable _currentInteractable;
    private InventorySystem _inventorySystem;

    private void Start()
    {
        _inventorySystem = GetComponent<InventorySystem>();
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
                _interactionText.text = _currentInteractable.InteractText;
        }
    }

    /// <summary>
    /// Function that calls the interact event on the object the player is looking at
    /// </summary>
    public void Interact(CallbackContext context)
    {
        if (!context.performed || _hit.collider == null) 
            return;

        _hit.collider.GetComponent<IInteract>()?.OnInteract();

        if (_hit.collider.TryGetComponent(out Item item))
            _inventorySystem.Pickup(item);
    }

    public void Interact()
    {
        _hit.collider.GetComponent<IInteract>()?.OnInteract();

        interactCD = 3;

        if (_hit.collider.TryGetComponent(out Item item))
            _inventorySystem.Pickup(item);
    }
}