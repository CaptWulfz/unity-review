using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    void Awake()
    {
        AudioManager.Instance.Initialize();
        PopupManager.Instance.Initialize();
    }

}
