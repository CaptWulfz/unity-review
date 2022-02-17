using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied(Parameters param = null)
    {
        if (param != null)
        {
            string text = param.GetParameter<string>("deathText", "I'm fine");
            OnButtonClicked(text);
        }
    }

    #region Unity Button Events
    public void OnButtonClicked(string text = "This is the menu!")
    {
        MenuPopup popup = PopupManager.Instance.ShowPopup<MenuPopup>("MenuPopup");
        popup.Setup(text);
        popup.Show();
    }
    #endregion
}
