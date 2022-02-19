using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingOverlay : Singleton<LoadingOverlay>
{
    [SerializeField] GameObject overlay;
    [SerializeField] Text loadingText;

    private bool isOverlayActive = false;
    public void ShowOverlay(string loadingText)
    {
        if (!this.isOverlayActive)
        {
            InputManager.Instance.GetControls.Player.Disable();
            this.overlay.SetActive(true);
            this.loadingText.text = loadingText;
            this.isOverlayActive = true;
        }
    }

    public void HideOverlay()
    {
        if (this.isOverlayActive)
        {
            InputManager.Instance.GetControls.Player.Enable();
            this.overlay.SetActive(false);
            this.loadingText.text = "";
            this.isOverlayActive = false;
        }
    }
}
