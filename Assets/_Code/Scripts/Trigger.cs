using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent TriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.Invoke();
    }
}
