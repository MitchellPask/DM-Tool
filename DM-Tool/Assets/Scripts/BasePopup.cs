using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    virtual public void Open()
    {
        Debug.Log(this + ".Open()");
        if (!IsActive())
        {
            this.gameObject.SetActive(true);
            Messenger.Broadcast(GameEvent.POPUP_OPENED);
        }
        else
        {
            Debug.LogError(this + ".Open() - trying to open a dialog that is active!");
        }
    }

    virtual public void Close()
    {
        Debug.Log(this + ".Close()");
        if (IsActive())
        {
            this.gameObject.SetActive(false);
            Messenger.Broadcast(GameEvent.POPUP_CLOSED);
        }
        else
        {
            Debug.LogError(this + ".Close() - trying to close a dialog that is not active!");
        }
    }

    virtual public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }
}
