using UnityEngine;

public class RadioHoverDetector : MonoBehaviour
{
    public AudioSource staticNoiseAudioSource;
    public GameObject Antenna;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = Antenna.transform.localScale;
    }

    void OnMouseEnter()
    {
        if (CompareTag("radio"))
        {
            // scale of the antenna increases
            Antenna.transform.localScale = originalScale * 1.5f;
            // play the static noise
            staticNoiseAudioSource.Play();
        }
    }

    void OnMouseExit()
    {
        if (CompareTag("radio"))
        {
            // scale of the antenna resets
            Antenna.transform.localScale = originalScale;
            // stop playing the static noise
            staticNoiseAudioSource.Stop();
        }
    }
}
