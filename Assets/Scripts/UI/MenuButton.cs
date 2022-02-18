using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{

    
    #region Unity Button Events
    public void OnButtonClicked(){
        MenuPopup popup = PopupManager.Instance.ShowPopup<MenuPopup>("MenuPopup");
        popup.Setup("Wololo Menu");
        popup.Show();
    }
    #endregion
}
