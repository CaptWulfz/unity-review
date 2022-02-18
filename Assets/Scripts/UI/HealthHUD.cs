using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] Slider health;

    private void OnEnable() {
        
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
    }

    private void OnHealthModified(Parameters param = null){

            if (param != null){
                float newHealthValue = param.GetParameter<float>("newHealth", 100);
                health.value = newHealthValue / 100;

                if(health.value <= 0f)
                {
                    MenuPopup popup = PopupManager.Instance.ShowPopup<MenuPopup>("MenuPopup");
                    popup.Setup("DED");
                    popup.Show();
                }
            }
    }
}
