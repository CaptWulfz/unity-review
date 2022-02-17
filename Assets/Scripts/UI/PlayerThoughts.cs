using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThoughts : MonoBehaviour
{
    [SerializeField] Text thoughtsText;

    void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
    }

    void OnHealthModified(Parameters param = null)
    {
        if (param != null)
        {
            string thoughts = param.GetParameter<string>("thoughts", "fine.");
            this.thoughtsText.text = thoughts;
        }
    }
}
