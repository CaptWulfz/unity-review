using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] string sourceId;

    // onEnable = Checkbox in Object
    private void onEnable()
    {
        AudioManager.Instance.RegisterAudioSource(AudioKeys.SFX, this.sourceName, this.source);
        AudioManager.Instance.RegisterAudioSource(AudioKeys.MUSIC, this.sourceName, this.source);
    }

    // onDisabled uncheck in Object
    private void onDisable()
    {
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.SFX, this.sourceName);
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.MUSIC, this.sourceName);
    }

    protected override void Start()
    {
        this.sourceName = string.Format("Player1Source@{0}", Guid.NewGuid());
        this.sourceId = this.sourceName;
        onEnable();
        this.speed = 10f;
        base.Start();   
    }

    // Update is called once per frame
    //protected override void Update()
    //{
    //    base.Update();
    //}
   
    private void Update()
    {

        // Centralized Audio Manager
        if (Keyboard.current.aKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.COIN);
            Debug.Log("GOING LEFT");
        }

        if (Keyboard.current.wKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.JUMP);
            Debug.Log("GOING LEFT");
        }

        if (Keyboard.current.dKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.MUSIC, this.sourceName, MusicKeys.CHILL);
            Debug.Log("GOING RIGHT");
        }
    }

}
