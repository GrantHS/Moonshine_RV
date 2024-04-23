using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RadioHipHopController : MonoBehaviour
{
    public GameObject countryController;
    public GameObject JazzController;
    public GameObject radioPanel;
    public Button playButton;
    public Button pauseButton;
    public AudioClip[] songs; // array to store songs
    public AudioSource audioSource;

    // Placeholder for the current song index
    private int currentSongIndex = 0;
    private bool isPlaying = false;
    private int repeatMode = 0; // 0: no repeat, 1: repeat once, 2: repeat forever


    void Start()
    {
        radioPanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            radioPanel.SetActive(false);
        }
    }

    public void Exit()
    {
        radioPanel.SetActive(false);
    }

    // play functionality
    public void Play()
    {
        Debug.Log("Playing song...");
        isPlaying = true;
        audioSource.clip = songs[currentSongIndex]; // set the current song
        audioSource.Play(); // start playing the song
        // show pause button, hide play button
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    // Pause functionality
    public void Pause()
    {
        Debug.Log("Pausing song...");
        isPlaying = false;
        audioSource.Pause(); // pause the song
        // show the play button, hide the pause button
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    // Skip functionality
    public void Skip()
    {
        Debug.Log("Skipping to the next song...");
        // increment song index
        currentSongIndex = (currentSongIndex + 1) % songs.Length; //loop around to the beginning if at the end
        if (isPlaying)
        {
            Play(); // if the song is playing, play the next song
        }
    }

    // Go back functionality
    public void GoBack()
    {
        Debug.Log("Going back to the previous song...");
        //decrement current song index 
        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length; //loop around to the end if at the beginning
        if (isPlaying)
        {
            Play(); //if the song is playing, play the previous song
        }
    }

    public void Repeat()
    {
        repeatMode = (repeatMode + 1) % 3; // this just cycle through 0, 1, 2
        switch (repeatMode)
        {
            case 0:
                Debug.Log("Repeat turned off");
                audioSource.loop = false;
                break;
            case 1:
                Debug.Log("Repeating song once");
                audioSource.loop = false;
                break;
            case 2:
                Debug.Log("Repeating song forever");
                audioSource.loop = true;
                break;
        }
    }

    public void SwitchToCountry()
    {
        // turn off the hip-hop radio panel and stop the music
        radioPanel.SetActive(false);
        audioSource.Stop();

        //turn on the countrymusic panel
        countryController.SetActive(true);
        radioPanel.SetActive(false);
    }

    public void SwitchToJazz()
    {
        // turn off the hip-hop radio panel and stop the music
        radioPanel.SetActive(false);
        audioSource.Stop();

        //turn on the countrymusic panel
        JazzController.SetActive(true);
        radioPanel.SetActive(false);
    }
}
