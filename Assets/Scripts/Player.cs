using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5.0f;
    [SerializeField] private float m_Jump_Speed = 10.0f;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] float health = 100f;
    [SerializeField] AudioSource audioSource;

    Controls controls;

    string sourceid;
    string sourceName;

    private void OnEnable()
    {
        AudioManager.Instance.RegisterAudioSource(AudioKeys.SFX, this.sourceName, this.audioSource);
        AudioManager.Instance.RegisterAudioSource(AudioKeys.MUSIC, this.sourceName, this.audioSource);
    }
    void OnDisable()
    {
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.SFX, this.sourceName);
        AudioManager.Instance.UnregisterAudioSource(AudioKeys.MUSIC, this.sourceName);
    }

    void Start()
    {
        this.sourceName = "Player Source" + this.gameObject.name;
        Debug.Log(this.gameObject.name);
        this.sourceid = sourceName;

        OnEnable();

        this.controls = new Controls();
        this.controls.Player.Enable();

        playerRigidbody.freezeRotation = true;
        playerRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMovementbyRigidbody();

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Parameters param = new Parameters();
            this.health -= 10f;
            param.AddParameter<float>("newHealthValue", this.health);
            param.AddParameter<string>("thoughts", "dmgd");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);

            if (this.health <= 0)
            {
                Parameters param2 = new Parameters();
                param2.AddParameter<string>("deathText", "Deadd");
                EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_DIED, param2);
            }
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Parameters param = new Parameters();
            this.health += 10f;
            param.AddParameter<float>("newHealthValue", this.health);
            param.AddParameter<string>("thoughts", "heald");
            EventBroadcaster.Instance.PostEvent(EventNames.ON_HEALTH_MODIFIED, param);
        }

        //if (Keyboard.current.dKey.isPressed)
        //    AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.YEE);

        //if (Keyboard.current.aKey.isPressed)
        //    AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.SMALLAHH);

        //if (Keyboard.current.wKey.isPressed)
        //    AudioManager.Instance.PlayAudio(AudioKeys.SFX, this.sourceName, SFXKeys.LOUDAHH);

        //if (Keyboard.current.sKey.isPressed && this.gameObject.name == "Player2")
        //    AudioManager.Instance.PlayAudio(AudioKeys.MUSIC, this.sourceName, MusicKeys.BGM);

        //if (this.controls.Player.Jump.WasPressedThisFrame())
        //    playerRigidbody.velocity = Vector2.up * m_Speed * m_Jump_Speed;
    }

    void PlayerMovementbyRigidbody()
    {
        Vector2 movement = controls.Player.Movement.ReadValue<Vector2>();
        playerRigidbody.velocity = movement * m_Speed;
    }

    void PlayerMovementbyTransform()
    {
        Vector2 move = this.transform.position;

        if (Keyboard.current.dKey.isPressed)
            move = new Vector2(this.transform.position.x + m_Speed * Time.deltaTime, this.transform.position.y);
        if (Keyboard.current.aKey.isPressed)
            move = new Vector2(this.transform.position.x - m_Speed * Time.deltaTime, this.transform.position.y);
        if (Keyboard.current.wKey.isPressed)
            move = new Vector2(this.transform.position.x, this.transform.position.y + m_Speed * Time.deltaTime);
        if (Keyboard.current.sKey.isPressed)
            move = new Vector2(this.transform.position.x, this.transform.position.y - m_Speed * Time.deltaTime);

        this.transform.position = move;
    }
}
