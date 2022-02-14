using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : Popup
{
    [Header("Popup Components")]
    [SerializeField] Text frontText;

    #region Overrides
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
    #endregion

    public void Setup(string frontText)
    {
        this.frontText.text = frontText;
    }
}
