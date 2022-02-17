using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_DIED, OnPlayerDies);
    }

    private void OnPlayerDies(Parameters param = null)
    {
        if (param != null)
        {
            string text = param.GetParameter<string>("deathText", "Nothing");
            onButtonClicked(text);
        }
    }

    #region Unity Button Events
    public void onButtonClicked(string text = "This is the menu!")
    {
        MenuPopup popup = PopupManager.Instance.ShowPopup<MenuPopup>("MenuPopup");
        popup.Setup(text);
        popup.Show();
    }

    #endregion

    #region Unity Conversation Subtitle

    //[SerializeField] Panel panel;
    //[SerializeField] string[] textWords;
    //public void OnButtonClicked()
    //{
    //    //string[] arr = { "hello" };
    //    this.panel.Setup(this.textWords);
    //    this.panel.gameObject.SetActive(true);
    //}
    #endregion

}
