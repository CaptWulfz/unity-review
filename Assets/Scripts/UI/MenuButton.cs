using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_DIED, OnPlayerDies);
    }

    void OnPlayerDies(Parameters param = null)
    {
        if (param != null)
        {
            string text = param.GetParameter<string>("deathText", "none");
            OnButtonClicked(text);
        }
    }

    #region Unity Button Events
    public void OnButtonClicked(string text = "Main Menu")
    {
        MainMenuPopUp popUp = PopUpManager.Instance.ShowPopUp<MainMenuPopUp>("Main Menu");
        popUp.Setup(text);
        popUp.Show();
    }
    #endregion
}
