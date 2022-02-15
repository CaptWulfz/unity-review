using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5.0f;
    [SerializeField] private float m_Jump_Speed = 10.0f;
    [SerializeField] Rigidbody2D playerRigidbody;

    Controls controls;

    void Start()
    {
        this.controls = new Controls();
        this.controls.Player.Enable();

        playerRigidbody.freezeRotation = true;
        //playerRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMovementbyRigidbody();

        if (this.controls.Player.Jump.WasPressedThisFrame())
            playerRigidbody.velocity = Vector2.up * m_Speed * m_Jump_Speed;
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
