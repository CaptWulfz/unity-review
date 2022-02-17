using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton if Manager
public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] Canvas popupCanvas;

    private const string POPUPS_PATH = "Prefabs/UI/{0}";

    public void Initialize()
    {
        this.popupCanvas = GameObject.FindGameObjectWithTag(TagNames.POPUP_CANVAS).GetComponent<Canvas>();
    }

    public T ShowPopup<T>(string popupName)
    {
        // for Future references of naming
        //string test = string.Format("{0} I am {1}", "hello", "jerome");

        string path = string.Format(POPUPS_PATH, popupName);
        GameObject popupObj = Resources.Load<GameObject>(path);
        popupObj.SetActive(false);
        GameObject deploy = GameObject.Instantiate(popupObj, this.popupCanvas.transform);

        return deploy.GetComponent<T>();

    }

}
