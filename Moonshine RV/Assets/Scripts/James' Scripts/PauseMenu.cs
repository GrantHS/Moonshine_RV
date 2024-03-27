using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Checks if the Escape key is pressed
        {
            if (isPaused)
            {
                Resume(); // If the game is paused, resume
            }
            else
            {
                Pause(); // If the game is not paused, pause it
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Deactivates the pause menu UI
        Time.timeScale = 1f; // Resumes the game time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Activates the pause menu UI
        Time.timeScale = 0f; // Freezes the game time
        isPaused = true;
    }

    // Optionally, you can add more functions here to handle other menu actions, like quitting the game or loading other scenes.
}
