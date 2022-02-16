using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Rigidbody2D playerRigidbody;
    Controls controls;

    void Start() {
        this.controls = new Controls();
        this.controls.Player.Enable();
    }
    
    void Update()
    {
        MoveByRigidBody();
    }

    private void MoveByRigidBody(){
        Vector2 movement = controls.Player.Movement.ReadValue<Vector2>();
        playerRigidbody.velocity = movement * speed;
    }

    private void MoveByTransform(){
        Vector2 move = this.transform.position;

        //Left
        if (Keyboard.current.aKey.isPressed)
            move = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y);
        //Right
        if (Keyboard.current.dKey.isPressed)
            move = new Vector2(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y);
        //Up
        if (Keyboard.current.wKey.isPressed)
            move = new Vector2(this.transform.position.x , this.transform.position.y + speed * Time.deltaTime);
        //Down
        if (Keyboard.current.sKey.isPressed)
            move = new Vector2(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime);
        
        this.transform.position = move;
    }
}
