using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreneManager : MonoBehaviour
{
    public void ReplayGame()
    {
        Debug.Log("Restart Game");
        //SceneManager.LoadScene("MainScene"); UNCOMMENT THIS WHEN READY

    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}