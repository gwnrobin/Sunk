using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // The intensity of the shake
    [SerializeField]
    private float shakeIntensity = 0.1f;
    // The duration of the shake
    [SerializeField]
    private float shakeDuration = 0.5f;

    // The original position of the camera
    Vector3 originalPos;

    public void Shake()
    {
        // Start a coroutine that shakes the camera
        originalPos = transform.localPosition;
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        // Shake the camera for the specified duration
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            // Randomly offset the camera's position
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera's position
        transform.localPosition = originalPos;
    }
}