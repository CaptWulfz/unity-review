using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private Controls controls = null;

    private bool isDone = false;
    public bool IsDone
    {
        get { return this.isDone; }
    }

    public void Initialize()
    {
        if (controls == null)
            controls = new Controls();

        StartCoroutine(WaitForInitialization());
    }

    public IEnumerator WaitForInitialization()
    {
        yield return new WaitUntil(() => { return this.controls != null; });
        this.isDone = true;
    }

    public Controls GetControls()
    {
        if (this.controls == null)
            Initialize();

        return this.controls;
    }
}
