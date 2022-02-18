using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] Text textStatus;

    private void OnEnable() {
        
        EventBroadcaster.Instance.AddObserver(EventNames.ON_HEALTH_MODIFIED, OnHealthModified);
    }

    private void OnHealthModified(Parameters param = null){

            if (param != null){
                string status = param.GetParameter<string>("playerStatus", "Normal.");
                this.textStatus.text = status;
            }
    }
}
