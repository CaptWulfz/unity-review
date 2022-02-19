using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    [SerializeField] SplashScreen splashScreen;

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


        ///GAME MANAGER HERE
        GameManager.Instance.Initialize();
        yield return new WaitUntil(() => { return GameManager.Instance.IsDone; });
        this.splashScreen.SetLoadingProgress(1f);

        yield return new WaitForSeconds(1.5f);
        this.splashScreen.Hide();


    }

}
