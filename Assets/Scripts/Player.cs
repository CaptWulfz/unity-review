using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : Entity
{

    [SerializeField] string sourceId;

    private float health = 100f;

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

    protected override void MoveByRigidBody(){
        base.MoveByRigidBody();
/*
        if(this.playerRigidbody.velocity != Vector2.zero)
            this.animator.SetFloat("Speed",1f);
        else
            this.animator.SetFloat("Speed", 0f);
*/
        if(this.playerRigidbody.velocity.x < 0)
            this.spriteRenderer.flipX = true;
        else if (this.playerRigidbody.velocity.x > 0)
            this.spriteRenderer.flipX = false;

    }

    private void FreeMoveAnimation(){
        float speedParam = Mathf.Abs(this.playerRigidbody.velocity.x + this.playerRigidbody.velocity.y);
        this.animator.SetFloat("Speed",speedParam);
    }
    
    protected override void Update()
    {
        

        if (Keyboard.current.aKey.isPressed)
        {            
            //AudioManager.Instance.PlayAudioOnPlayer(AudioKeys.SFX, SFXKeys.GIB);
        }
        if (Keyboard.current.wKey.isPressed)
        {
            //AudioManager.Instance.PlayAudioOnPlayer(AudioKeys.SFX, SFXKeys.STOP);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            //AudioManager.Instance.PlayAudioOnPlayer(AudioKeys.MUSIC, MusicKeys.CHILL);
        }
        //MoveByRigidBody();

        base.Update();
        
        FreeMoveAnimation();
    }
   
   
private void DamagePlayer(){

    Parameters param = new Parameters();
            this.health -=10;
            param.AddParameter<float>("newHealth", this.health);
            param.AddParameter<string>("playerStatus", "Injured.");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);
}

private void HealPlayer(){
    Parameters param = new Parameters();
            this.health +=10;
            param.AddParameter<float>("newHealth", this.health);
            param.AddParameter<string>("playerStatus", "Recovering.");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);
}

}
