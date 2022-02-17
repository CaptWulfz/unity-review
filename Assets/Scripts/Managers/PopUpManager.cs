using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] Canvas popUpCanvas;

    private const string POPUP_PATH = "Prefabs/UI/{0}";

    public void Initialize()
    {
        this.popUpCanvas = GameObject.FindGameObjectWithTag(TagNames.POPUP_CANVAS).GetComponent<Canvas>();
    }

    public T ShowPopUp<T>(string popUpName)
    {
        string path = string.Format(POPUP_PATH, popUpName);
        GameObject popUpObject = Resources.Load<GameObject>(path);
        popUpObject.SetActive(false);
        GameObject deploy = GameObject.Instantiate(popUpObject, this.popUpCanvas.transform);

        return deploy.GetComponent<T>();
    }
}
