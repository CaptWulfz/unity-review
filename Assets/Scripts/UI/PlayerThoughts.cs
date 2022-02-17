using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThoughts : MonoBehaviour
{
    [SerializeField] Text thoughtsText;

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModiefe);
    }

    private void OnHealthModiefe(Parameters param = null)
    {
        if (param != null)
        {
            string thoughts = param.GetParameter<string>("thoughts", "I'm alright");
            this.thoughtsText.text = thoughts;
        }
    }
}
