using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Need at least 1 yield return function for every IEnumerator
    public static IEnumerator WaitForAnimation(Animation anim, Action onComplete = null)
    {
        yield return new WaitUntil(() => { return !anim.isPlaying; });

        // if may laman yung onComplete... call Invoke()
        onComplete?.Invoke();
    }

}
