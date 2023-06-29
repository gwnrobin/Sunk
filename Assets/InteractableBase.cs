using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    [SerializeField]
    private string interactText;
    public string InteractText => interactText;
}
