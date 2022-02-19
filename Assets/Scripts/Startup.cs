using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    [SerializeField] SplashScreen splashScreen;
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        AudioManager.Instance.Initialize();
        yield return new WaitUntil(() => { return AudioManager.Instance.IsDone; });
        this.splashScreen.SetLoadingProgress(0.25f);

        PopupManager.Instance.Initialize();
        yield return new WaitUntil(() => { return PopupManager.Instance.IsDone; });
        this.splashScreen.SetLoadingProgress(0.50f);

        InputManager.Instance.Initialize();
        yield return new WaitUntil(() => { return InputManager.Instance.IsDone; });
        this.splashScreen.SetLoadingProgress(0.75f);

        GameManager.Instance.Initialize();
        yield return new WaitUntil(() => { return GameManager.Instance.IsDone; });
        this.splashScreen.SetLoadingProgress(1f);

        yield return new WaitForSeconds(2f);
        this.splashScreen.Hide();

        this.mainCamera.transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(this.mainCamera.gameObject, SceneManager.GetActiveScene());
    }

}
