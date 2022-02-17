using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] string sourceId;

    [SerializeField] float health = 100f;

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
   
    private void Update()
    {

        // Centralized Audio Manager
        if (Keyboard.current.kKey.wasReleasedThisFrame)
        {
            Parameters param = new Parameters();
            this.health -= 10;
            param.AddParameter<float>("newHealth", this.health);
            param.AddParameter<string>("playerThoughts", "ah shit, here we go again");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);


            if (this.health <= 0f)
            {
                Parameters param2 = new Parameters();
                param2.AddParameter<string>("deathText", "You have died!");
                EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_DIED, param2);

                //MenuPopup popup = PopupManager.Instance.ShowPopup<MenuPopup>("MenuPopup");
                //popup.Setup("You have died!");
                //popup.Show();
            }

            //AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.COIN);
            Debug.Log("DAMAGE HEALTH");
        }

        if (Keyboard.current.mKey.isPressed)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.JUMP);
            Debug.Log("HEAL HEALTH");
        }

        if (Keyboard.current.lKey.wasReleasedThisFrame)
        {
            Parameters param = new Parameters();
            this.health += 10;
            param.AddParameter<float>("newHealth", this.health);
            param.AddParameter<string>("playerThoughts", "Damn, that hit the spot");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);

            //AudioManager.Instance.PlayAudio(AudioKeys.MUSIC, this.sourceName, MusicKeys.CHILL);
            Debug.Log("GOING RIGHT");
        }

        base.Update();
    }

}
