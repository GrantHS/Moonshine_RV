using UnityEngine;

public class RadioHoverDetector : MonoBehaviour
{
    public AudioSource staticNoiseAudioSource;
    private bool isHovering = false;

    void OnMouseEnter()
    {
        if (CompareTag("radio"))
        {
            isHovering = true;
            // when you hover your mouse pointer over the radio it plays a static noise
            staticNoiseAudioSource.Play();
        }
    }

    void OnMouseExit()
    {
        if (CompareTag("radio"))
        {
            isHovering = false;
            // when you stop hovering your mouse pointer over the radio it stops playing the static noise
            staticNoiseAudioSource.Stop();
        }
    }
}
