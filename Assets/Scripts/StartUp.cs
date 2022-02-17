using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AudioManager.Instance.Initialize();
        PopupManager.Instance.Initialize();
    }

}
