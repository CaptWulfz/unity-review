using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] Slider health;

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
    }

    private void OnHealthModified(Parameters param = null)
    {
        if (param != null)
        {
            float newHealthValue = param.GetParameter<float>("newHealthValue", 100f);
            string thoughts = param.GetParameter<string>("thoughts", "I'm alright");
            Debug.Log("Thoughts: " + thoughts);
            this.health.value = newHealthValue / 100;
            if (this.health.value <= 0f)
            {
                //EventBroadcaster.Instance.RemoveObserver(EventNames.ON_HEALTH_MODIFIED);
            }
        }
    }
}
