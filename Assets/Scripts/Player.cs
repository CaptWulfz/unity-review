using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] float health = 100f;

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
        base.Start();
        this.sourceName = string.Format("PlayerSource@{0}", Guid.NewGuid());
        OnEnable();
        this.controls.Player.Enable();
    }

    protected override void Update()
    {
        Move();

        if (Keyboard.current.qKey.wasReleasedThisFrame)
        {
            LoadingOverlay.Instance.ShowOverlay("Loading");
        }

        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            LoadingOverlay.Instance.HideOverlay();
        }
    }

    private void Move()
    {
        Vector2 move = this.rb.velocity;
        move.x = this.controls.Player.Movement.ReadValue<Vector2>().x;
        move.y = TryJump(this.rb.velocity.y);

        MoveByRigidBody(move);
    }

    private float TryJump(float defaultMov)
    {
        float yMov = defaultMov;
        //if (jump)
        //   yMov = 10f;

        return yMov;
    }

    protected override void MoveByRigidBody(Vector2 move)
    {
        base.MoveByRigidBody(move * speed);

        FreeMoveAnimation();

        if (this.rb.velocity.x < 0)
            this.spriteRenderer.flipX = true;
        else if (this.rb.velocity.x > 0)
            this.spriteRenderer.flipX = false;
    }

    private void FreeMoveAnimation()
    {
        float speedParam = Mathf.Abs(this.rb.velocity.x + this.rb.velocity.y);
        this.animator.SetFloat("Speed", speedParam);
    }

    private void PlatformerMoveAnimation()
    {
        this.animator.SetFloat("Speed", Mathf.Abs(this.rb.velocity.x));
    }

    private void DamagePlayer()
    {
        Parameters param = new Parameters();
        this.health -= 10f;
        param.AddParameter<float>("newHealthValue", this.health);
        param.AddParameter<string>("thoughts", "Shit");
        EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);

        if (this.health <= 0)
        {
            Parameters param2 = new Parameters();
            param2.AddParameter<string>("deathText", "You have Died!");

            EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_DIED, param2);
        }
    }

    private void HealPlayer()
    {
        Parameters param = new Parameters();
        this.health += 10f;
        param.AddParameter<float>("newHealthValue", this.health);
        param.AddParameter<string>("thoughts", "I'm alive");
        EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);
    }
}
