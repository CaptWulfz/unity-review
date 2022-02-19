using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    Controls controls;
    public Controls GetControls
    {
        get { return this.controls; }
    }

    private bool isDone = false;
    public bool IsDone
    {
        get { return this.isDone; }
    }

    public void Initialize()
    {
        if (this.controls == null)
            this.controls = new Controls();

        StartCoroutine(WaitForInitialization());
    }
    private IEnumerator WaitForInitialization()
    {
        yield return new WaitUntil(() => { return this.controls != null; });
        this.isDone = true;
    }
}
