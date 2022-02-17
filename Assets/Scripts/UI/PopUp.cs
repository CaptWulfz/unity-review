using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public void Show()
    {
        this.gameObject.SetActive(true);
    }    
    public void Hide()
    {
        Destroy(this.gameObject);
    }
    public void OnCloseButtonClicked()
    {
        Hide();
    }
}
