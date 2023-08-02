using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    // Object References
    public GameObject instructionScreen;

    void Start()
    {
        instructionScreen.SetActive(false);
    }

    void Update()
    {
        if(instructionScreen.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Space))
        {
            //SceneManager.LoadScene("MainScene"); UNCOMMENT THIS WHEN READY
            Debug.Log("Start Game");
        }
    }

    public void StartGame()
    {
        instructionScreen.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
