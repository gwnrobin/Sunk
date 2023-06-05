using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Flickering : MonoBehaviour
{
    public UnityEvent FlickerOn;
    public UnityEvent FlickerOff;

    private float timeDelay;

    public void StartFlickering(float amount)
    {
        StartCoroutine(Flicker(amount));
    }

    public IEnumerator Flicker(float amount)
    {
        float times = 0;
        while (times < amount)
        {
            FlickerOn?.Invoke();
            timeDelay = Random.Range(0.05f, 0.3f);
            yield return new WaitForSeconds(timeDelay);
            FlickerOff?.Invoke();
            timeDelay = Random.Range(0.05f, 0.3f);
            yield return new WaitForSeconds(timeDelay);
            times++;
        }
    }
}
