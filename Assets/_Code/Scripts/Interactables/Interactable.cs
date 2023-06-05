using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteract
{
    public UnityEvent onInteract;//unity event that gets activated when a player interacts

    [SerializeField]
    private string interactText;
    public string InteractText => interactText;

    public virtual void OnInteract()
    {
        onInteract?.Invoke();
    }
}