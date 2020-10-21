using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsPopup : BasePopup
{
    [SerializeField]
    private UIController UIController;
    [SerializeField]
    private SettingsPopup SettingsPopup;


    public void OnSettingsButton()
    {
        base.Close();
        SettingsPopup.Open();
    }

    public void OnExitGameButton()
    {
        Application.Quit();
    }

    public void OnReturnToGameButton()
    {
        base.Close();
        UIController.ResumeGame();
    }

    public void OnMainMenu()
    {
        UIController.ResumeGame();
        SceneManager.LoadScene(0);
    }
}
