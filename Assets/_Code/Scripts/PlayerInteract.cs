using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInteract : MonoBehaviour
{
    /*
    [SerializeField]
    private TMP_Text interactionText;

    [SerializeField]
    private int interactRange = 3;

    private Transform playerCamera;
    //private PlayerInventory inventory;

    private RaycastHit hit;

    private void Awake()
    {
        playerCamera = Camera.main.transform;
       // inventory = GetComponent<PlayerInventory>();
    }

    private void FixedUpdate()
    {
        UpdateRaycast();
    }

    /// <summary>
    /// Updates the hit variable which is the object on the forward of the player camera
    /// </summary>
    private void UpdateRaycast()
    {
        ResetInteractionText();
        hit = default;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactRange))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                UpdateInteractionText(interactable);
            }
        }
    }

    /// <summary>
    /// Resets the interaction text
    /// </summary>
    private void ResetInteractionText()
    {
        interactionText.text = "";
    }

    /// <summary>
    /// Updates the interaction text based on the hit object
    /// </summary>
    /// <param name="interactable">The hit object</param>
    private void UpdateInteractionText(Interactable interactable)
    {
        interactionText.text = interactable.InteractText;
    }

    /// <summary>
    /// function that calls the interact event on the object the player is looking at
    /// </summary>
    public void Interact(CallbackContext context)
    {
        if (!context.performed || hit.collider == null) return;

        hit.collider.GetComponent<IInteract>()?.OnInteract();

        InteractWithPickupable();
        InteractWithDoor();
        InteractWithToPlace();
    }

    /// <summary>
    /// Handles interaction based on if the object is a pickupable
    /// </summary>
    private void InteractWithPickupable()
    {
        if (hit.collider.TryGetComponent(out Pickupable pickupable))
        {
            if (!pickupable.IsLocked)
                inventory.Add(pickupable);
        }
    }

    /// <summary>
    /// Handles interaction based on if the object is a door
    /// This uses the item you are holding to unlock the door if holding the correct item
    /// </summary>
    private void InteractWithDoor()
    {
        if (hit.collider.TryGetComponent(out LockedDoor door))
        {
            if (door.Interaction(inventory.Holding))
            {
                inventory.UseItem();
            }
        }
    }

    /// <summary>
    /// Handles interaction based on if the object is a ToPlace
    /// This places the item you are holding in the designated slot if it is the correct item
    /// </summary>
    private void InteractWithToPlace()
    {
        if (!hit.collider.TryGetComponent(out ToPlace placeable) || inventory.Holding is not Placeable)
            return;

        if (placeable.Interaction(inventory.Holding as Placeable))
        {
            Placeable item = inventory.Holding as Placeable;
            inventory.Drop();
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.position = placeable.transform.position;
            item.transform.rotation = placeable.transform.rotation;
        }
    }*/
}