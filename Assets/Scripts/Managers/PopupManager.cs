using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] Canvas popupCanvas;
    private const string POPUP_PATH = "Prefabs/UI/{0}";
    public void Initialize(){

        this.popupCanvas = GameObject.FindGameObjectWithTag(TagNames.POPUP_CANVAS).GetComponent<Canvas>();

    }

    public T ShowPopup<T>(string popupName){
        
        string path = string.Format(POPUP_PATH, popupName);
        GameObject popupObj = Resources.Load<GameObject>(path);
        popupObj.SetActive(false);
        GameObject deploy = GameObject.Instantiate(popupObj, this.popupCanvas.transform);

        return deploy.GetComponent<T>();
    }

    public void HidePopup(GameObject popup){
        Destroy(popup);
    }
}
