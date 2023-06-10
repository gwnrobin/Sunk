// Ignore Spelling: Interactable

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteract
{
    public UnityEvent onInteract;

    [SerializeField]
    private string interactText;
    public string InteractText => interactText;

    public virtual void OnInteract()
    {
        onInteract?.Invoke();
    }
}