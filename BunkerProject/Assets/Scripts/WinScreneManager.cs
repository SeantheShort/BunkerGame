using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreneManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void ReplayGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene("MainScene");

    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
