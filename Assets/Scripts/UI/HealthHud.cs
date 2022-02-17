using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHud : MonoBehaviour
{
    [SerializeField] Slider health;

    public void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, onHealthModified);
    }

    public void onHealthModified(Parameters param = null)
    {
        if (param != null)
        {
            float newHealthValue = param.GetParameter<float>("newHealth", 100);
            string thoughts = param.GetParameter<string>("thoughts", "I'm alright");
            this.health.value = newHealthValue / 100;
            Debug.Log("Thoughts: " + thoughts);
            if (this.health.value <= 0)
            {
                EventBroadcaster.Instance.RemoveObserverAtAction(EventNames.ON_HEALTH_MODIFIED, onHealthModified);
                //MenuPopup popup = PopupManager.Instance.ShowPopup<MenuPopup>("MenuPopup");
                //popup.Setup("You have died!");
                //popup.Show();
            }
        }
        
    }

}
