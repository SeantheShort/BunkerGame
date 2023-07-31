using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseUI;
    public bool isPaused;

    void Start()
    {
        isPaused = false;
        PauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) // Pauses Game (Sets time to 0)
        {
            isPaused = true;
            Time.timeScale = 0f;
            PauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) // Unpauses Game (Sets time to 1)
        {
            ResumeGame();
        }
    }
    public void ResumeGame() // Resume Game Button Function
    {
        isPaused = false;
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame() // Quit Game Button Function
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
