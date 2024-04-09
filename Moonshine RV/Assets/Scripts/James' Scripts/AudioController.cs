using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip; // Assign your audio clip in the Inspector

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If AudioSource component is not attached, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            StartCoroutine(PlaySoundRepeatedly());
        }
        else
        {
            Debug.LogError("Audio clip is not assigned!");
        }
    }

    IEnumerator PlaySoundRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // Wait for 60 seconds
            PlaySound();
        }
    }

    // Call this method to play the sound
    public void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
