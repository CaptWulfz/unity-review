using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] string sourceId;

    private void OnEnable()
    {
        AudioManager.Instance.RegisterAudioSource(AudioKeys.SFX, this.sourceName, this.source);
        AudioManager.Instance.RegisterAudioSource(AudioKeys.MUSIC, this.sourceName, this.source);
    }

    private void OnDisable()
    {
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.SFX, this.sourceName);
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.MUSIC, this.sourceName);
    }

    protected override void Start()
    {
        this.sourceName = string.Format("PlayerSource@{0}", Guid.NewGuid());
        this.sourceId = this.sourceName;
        OnEnable();
        this.speed = 10f;
        base.Start();
    }

    private void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.TOM);
        }

        if (Keyboard.current.sKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.NANI);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.MUSIC, this.sourceName, MusicKeys.CHILL);
        }
    }
}
