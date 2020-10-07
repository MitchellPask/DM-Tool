using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : BasePopup
{
    [SerializeField]
    private UIController UIController;
    [SerializeField]
    private OptionsPopup OptionsPopup;


    override public void Open()
    {
        base.Open();
    }

    public void OnOkayButton()
    {
        Close();
        OptionsPopup.Open();
    }

    public void OnCancelButton()
    {
        Close();
        OptionsPopup.Open();
    }

}
