using UnityEngine.Events;

public class Interactable : InteractableBase, IInteract
{
    public UnityEvent onInteract;

    public virtual void OnInteract()
    {
        onInteract?.Invoke();
    }
}