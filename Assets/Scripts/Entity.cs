using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D playerRigidbody;
    [SerializeField] protected AudioSource source;
    
    protected string sourceName;
    [SerializeField] protected float speed = 5f;
    Controls controls;


    protected virtual void Start() {
        this.controls = new Controls();
        this.controls.Player.Enable();
    }

    protected virtual void Update(){
        //MoveByRigidBody();
    }

    protected void MoveByRigidBody(){
        Vector2 movement = controls.Player.Movement.ReadValue<Vector2>();
        playerRigidbody.velocity = movement * speed;
    }

}
