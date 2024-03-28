using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // checks if the Escape key is pressed
        {
            if (isPaused)
            {
                Resume(); // if the game is paused, resume
            }
            else
            {
                Pause(); // if the game is not paused, pause it
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Deactivates the pause menu UI
        Time.timeScale = 1f; // resumes the game 
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // activates the pause menu UI
        Time.timeScale = 0f; // freezes the game 
        isPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
          