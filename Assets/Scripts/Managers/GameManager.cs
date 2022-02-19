using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Canvas hudCanvas;

    private bool isDone = false;
    public bool IsDone
    {
        get { return this.isDone; }
    }

    public void Initialize()
    {
        this.hudCanvas = GameObject.FindGameObjectWithTag(TagNames.HUD_CANVAS).GetComponent<Canvas>();
        StartCoroutine(WaitForInitialization());
    }

    private IEnumerator WaitForInitialization()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene1", LoadSceneMode.Single); ///LoadSceneMode.Additive for add new scene

        yield return new WaitUntil(() => { return asyncLoad.isDone; });


        this.isDone = true;
    }
}
