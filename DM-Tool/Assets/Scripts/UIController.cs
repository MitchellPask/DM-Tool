using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private OptionsPopup optionsPopup;
    [SerializeField]
    private SettingsPopup settingsPopup;
    private int popupsOpen = 0;

    void Awake()
    {
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && popupsOpen == 0)
        {
            PauseGame();
            optionsPopup.Open();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnPopupOpened()
    {
        popupsOpen++;
        Debug.Log("OnPopupOpened: " + popupsOpen);
    }

    private void OnPopupClosed()
    {
        popupsOpen--;
        Debug.Log("OnPopupClosed: " + popupsOpen);
    }

}
