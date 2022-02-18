using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    [Header("Physics Components")]
    [SerializeField] protected Rigidbody2D playerRigidbody;
    [SerializeField] protected AudioSource source;

    [Header("Animation Components")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float speed = 5f;

    protected string sourceName = "";
    Controls controls;


    protected virtual void Start() {
        this.controls = new Controls();
        this.controls.Player.Enable();
    }

    protected virtual void Update(){
        MoveByRigidBody();
    }

    protected virtual void MoveByRigidBody(){
        Vector2 movement = controls.Player.Movement.ReadValue<Vector2>();
        playerRigidbody.velocity = movement * speed;
    }

}
