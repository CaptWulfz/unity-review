using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] Canvas popupCanvas;

    private const string POPUP_PATH = "Prefabs/UI/{0}";

    private bool isDone = false;
    public bool IsDone
    {
        get { return this.isDone; }
    }

    public void Initialize()
    {
        this.popupCanvas = GameObject.FindGameObjectWithTag(TagNames.POPUP_CANVAS).GetComponent<Canvas>();
        StartCoroutine(WaitForInitialization());
    }

    private IEnumerator WaitForInitialization()
    {
        yield return new WaitUntil(() => { return this.popupCanvas != null; });
        this.isDone = true;
    }

    public T ShowPopup<T>(string popupName)
    {
        string path = string.Format(POPUP_PATH, popupName);
        GameObject popupObj = Resources.Load<GameObject>(path);
        popupObj.SetActive(false);
        GameObject deploy = GameObject.Instantiate(popupObj, popupCanvas.transform);

        return deploy.GetComponent<T>();
    }

    public void HidePopup(GameObject popup)
    {
        Destroy(popup);
    }
}
