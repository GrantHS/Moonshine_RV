using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickerLight; // Reference to the Light component
    public float minIntensity; // Minimum light intensity
    public float maxIntensity; // Maximum light intensity
    public float flickerSpeed; // How fast the light changes intensity

    private float random;

    void Start()
    {
        if (flickerLight == null)
        {
            flickerLight = GetComponent<Light>(); // Try to get the light component if not set
        }
    }

    void Update()
    {
        // Generate a random new intensity within the specified range
        random = Random.Range(minIntensity, maxIntensity);
        flickerLight.intensity = Mathf.Lerp(flickerLight.intensity, random, flickerSpeed * Time.deltaTime);
    }
}
