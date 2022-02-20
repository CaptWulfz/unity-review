using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] float health = 100f;
    [SerializeField] Camera cam;

    private bool isPlayerOnGround = true;
    [SerializeField] GroundChecker groundChecker;

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
        isPlayerOnGround = groundChecker.IsOnGround; // Ground Checker

        Vector2 move = this.rb.velocity;
        move.x = this.controls.Player.Movement.ReadValue<Vector2>().x * speed;

        Debug.Log("IS PLAYER ON THE GROUND: " + isPlayerOnGround);

        // Ground Check and Jump
        if (isPlayerOnGround)
        {
            this.controls.Player.Jump.Enable();
            if (this.controls.Player.Jump.WasPressedThisFrame())
            {
                AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.JUMP);
                this.controls.Player.Jump.Disable();
                move.y = jumpVelocity;
            }
        }
        PlatformerJumpAnimation(!isPlayerOnGround);
        MoveByRigidBody(move);
    }

    //private float PlayerJumpAction()
    //{
    //    if (isPlayerOnGround)
    //    {
    //        this.controls.Player.Jump.Enable();
    //        if (this.controls.Player.Jump.WasPressedThisFrame())
    //        {
    //            AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.JUMP);
    //            this.controls.Player.Jump.Disable();
    //            move.y = jumpVelocity;
    //            Debug.Log("JUMP");

    //        }
    //    }
    //}

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
    //private void FreeMoveAnimation()
    //{
    //    // for free movement up, down, left, right
    //    float speedParam = Mathf.Abs(this.rb.velocity.x + this.rb.velocity.y);
    //    this.animator.SetFloat("Speed", speedParam);
    //}

    private void PlatformerMoveAnimation()
    {
        // for left and right only
        this.animator.SetFloat("Speed", Mathf.Abs(this.rb.velocity.x));
    }

    private void PlatformerJumpAnimation(bool isJumping)
    {
        this.animator.SetBool("isJumping", isJumping);
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

}
