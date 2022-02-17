using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThoughts : MonoBehaviour
{
    [SerializeField] Text thoughtsText;

    public void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
    }
    private void OnHealthModified(Parameters param = null)
    {
        if (param != null)
        {
            string thoughts = param.GetParameter<string>("playerThoughts", "I'm alright");
            this.thoughtsText.text = thoughts;
        }
    }
}
