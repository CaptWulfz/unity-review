using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] Slider health;
    [SerializeField] float totalHealth = 100;

    void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
    }
    
    void OnHealthModified(Parameters param = null)
    {
        if (param != null)
        {
            float newHealthValue = param.GetParameter<float>("newHealthValue", 100f);
            this.health.value = newHealthValue / totalHealth;

            if (this.health.value <= 0)
            {
                EventBroadcaster.Instance.RemoveObserverAtAction(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
            }
        }
    }
}
