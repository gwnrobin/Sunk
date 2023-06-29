using UnityEngine.Events;

public class InteractableRequireItem : InteractableBase, IInteractRequire<Item>
{
    public UnityEvent<Item> onInteract;

    public virtual void OnInteract(Item item)
    {
        onInteract?.Invoke(item);
    }
}