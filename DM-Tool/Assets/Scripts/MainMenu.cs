using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void OnBuildButton()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        SceneManager.LoadScene("BuildEncounter");
    }

    public void OnStartButton()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("StartEncounter");
    }    

    public void OnQuitButton()
    {
        UnityEngine.Debug.Log("Quit button pressed.");
        Application.Quit();
    }
}
