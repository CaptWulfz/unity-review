using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] float health = 100f;
    [SerializeField] Camera cam;

    //[SerializeField] GameObject player;

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
        base.Start();
        this.sourceName = string.Format("Player1Source@{0}", Guid.NewGuid());
        onEnable();
        this.speed = 5f;
    }

    protected override void Update()
    {
        base.Update();
        PlayerMovementAction();


        // Player parent and camera child
        this.cam = GameObject.FindGameObjectWithTag(TagNames.MAIN_CAMERA).GetComponent<Camera>();
        this.cam.gameObject.transform.SetParent(this.gameObject.transform);
        // Player parent and camera child

        if (Keyboard.current.oKey.wasReleasedThisFrame)
        {
            LoadingOverlay.Instance.ShowOverlay("Loading");
        }

        if (Keyboard.current.pKey.wasReleasedThisFrame)
        {
            LoadingOverlay.Instance.HideOverlay();
        }
    }

    private void PlayerMovementAction()
    {
        Vector2 move = this.rb.velocity;
        move.x = this.controls.Player.Movement.ReadValue<Vector2>().x * speed;
        move.y = PlayerJumpAction(this.rb.velocity.y);

        if (this.controls.Player.Jump.WasPressedThisFrame())
        {
            move.y = jumpVelocity;
            //this.rb.velocity = Vector2.up * jumpVelocity;
            Debug.Log("JUMP");
        }
        MoveByRigidBody(move);
    }

    private float PlayerJumpAction(float defaultJump)
    {
        float jumpMovement = defaultJump;
        if(this.controls.Player.Jump.WasPressedThisFrame())
        {
            jumpMovement = jumpVelocity;
        }
        return jumpMovement;
        //AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.JUMP);

    }

    protected override void MoveByRigidBody(Vector2 move)
    {
        base.MoveByRigidBody(move);
        //PlayerJumpAction(move);

        PlatformerMoveAnimation();

        if (this.rb.velocity.x < 0)
            this.spriteRenderer.flipX = true;
        else if (this.rb.velocity.x > 0)
            this.spriteRenderer.flipX = false;

    }

    #region Animations
    private void FreeMoveAnimation()
    {
        // for free movement up, down, left, right
        float speedParam = Mathf.Abs(this.rb.velocity.x + this.rb.velocity.y);
        this.animator.SetFloat("Speed", speedParam);
    }

    private void PlatformerMoveAnimation()
    {
        // for left and right only
        this.animator.SetFloat("Speed", Mathf.Abs(this.rb.velocity.x));
    }

    private void PlatformerJumpAnimation()
    {

    }
    #endregion



    private void DamagePlayer()
    {
        // Damage
        Parameters param = new Parameters();
        this.health -= 10;
        param.AddParameter<float>("newHealth", this.health);
        param.AddParameter<string>("playerThoughts", "ah shit, here we go again");
        EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);
        AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.JUMP);
        Debug.Log("DAMAGE HEALTH");

        // DEATH
        if (this.health <= 0f)
        {
            Parameters param2 = new Parameters();
            param2.AddParameter<string>("deathText", "You have died!");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_DIED, param2);
            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.DOWN);
            Debug.Log("DEAD");
        }
    }

    private void HealPlayer()
    {
        Parameters param = new Parameters();
        this.health += 10;
        param.AddParameter<float>("newHealth", this.health);
        param.AddParameter<string>("playerThoughts", "Damn, that hit the spot");
        EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);

        AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.COIN);
        Debug.Log("GOING RIGHT");
    }

    private void JumpPlayer()
    {

        if (Keyboard.current.mKey.wasReleasedThisFrame)
        {
            AudioManager.Instance.PlayAudio(AudioKeys.MUSIC, this.sourceName, MusicKeys.CHILL);
            Debug.Log("MUSIC");
        }
    }

}
