using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] Animation anim;

    private const string POPUP_OPEN = "PopupOpen";
    private const string POPUP_CLOSE = "PopupClose";
    public void Show()
    {
        this.gameObject.SetActive(true);
        this.anim.Play(POPUP_OPEN);
    }
    public void Hide()
    {
        this.anim.Play(POPUP_CLOSE);
        AnimationHandler.WaitForAnimation(this.anim, () =>
        {
            PopupManager.Instance.HidePopup(this.gameObject);
        });
    }
    public void OnCloseButtonClicked()
    {
        Hide();
    }
}
