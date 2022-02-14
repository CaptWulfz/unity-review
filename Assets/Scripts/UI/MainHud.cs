using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHud : MonoBehaviour
{
    private void OnEnable()
    {
        
    }

    public void OnMenuButtonClicked()
    {
        MenuPopup menu = PopupManager.Instance.ShowPopup<MenuPopup>(PopupNames.MENU_POPUP);
        menu.Setup("This is the Menu!");
        menu.Show();
    }
}
