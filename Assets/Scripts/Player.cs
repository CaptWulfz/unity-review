using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : Entity
{

    [SerializeField] string sourceId;

    private void OnEnable() {
        AudioManager.Instance.RegisterAudioToSource(AudioKeys.SFX, this.sourceName, this.source);
        AudioManager.Instance.RegisterAudioToSource(AudioKeys.MUSIC, this.sourceName, this.source);
    }

    private void OnDisable() {
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.SFX, this.sourceName);
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.MUSIC, this.sourceName);
    }

    protected override void Start(){
    
    this.sourceName = string.Format("PlayerSource@{0}", Guid.NewGuid());
    this.sourceId = this.sourceName;
    AudioManager.Instance.RegisterAudioToSource(AudioKeys.SFX, this.sourceName, this.source);
    AudioManager.Instance.RegisterAudioToSource(AudioKeys.MUSIC, this.sourceName, this.source);
    this.speed = 10f;
    base.Start();
    }
    
    protected override void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            AudioManager.Instance.PlayAudioOnPlayer(AudioKeys.SFX, SFXKeys.GIB);
        }
        if (Keyboard.current.wKey.isPressed)
        {
            AudioManager.Instance.PlayAudioOnPlayer(AudioKeys.SFX, SFXKeys.STOP);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            AudioManager.Instance.PlayAudioOnPlayer(AudioKeys.MUSIC, MusicKeys.CHILL);
        }
        //MoveByRigidBody();

        base.Update();
    }
   
}
