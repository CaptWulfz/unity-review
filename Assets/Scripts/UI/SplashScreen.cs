using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Slider loadingBar;
    [SerializeField] Text progressText;

    public void SetLoadingProgress(float progress)
    {
        this.loadingBar.value = progress;

    }
    
    public void Hide()
    {
        Destroy(this.gameObject);
    }

    public void onSliderValueChange()
    {
        float progress = this.loadingBar.value * 100;
        this.progressText.text = string.Format("{0}%", progress);
    }

}
