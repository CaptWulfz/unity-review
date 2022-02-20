using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Entity : MonoBehaviour
{
    [Header("Physics Components")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected AudioSource source;

    [Header("Animation Components")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;

    protected string sourceName = "";
    protected float speed = 5f;
    
    protected Controls controls;

    #region for jumping purposes
    protected float jumpVelocity = 10f;
    #endregion

    protected virtual void Start()
    {
        this.controls = InputManager.Instance.GetControls;
        this.controls.Player.Enable();
    }
    protected virtual void Update()
    {
        //MoveByRigidBody();
        //isGrounded = Physics2D.OverlapCircle(groundPoint.position, 2f, whatIsGround);

    }

    protected virtual void MoveByRigidBody(Vector2 move)
    {
        this.rb.velocity = move;

    }

    #region Move by Transform (Keyboard)
    //protected void MoveByTransform()
    //{
    //    Vector2 move = this.transform.position;
    //    // Input.GetKeyUp(KeyCode.A)
    //    // Moving Left
    //    // deltaTime is based when
    //    if (Keyboard.current.aKey.isPressed)
    //    {
    //        move = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y);
    //        Debug.Log("GOING LEFT");
    //    }
    //    if (Keyboard.current.dKey.isPressed)
    //    {
    //        move = new Vector2(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y);
    //        Debug.Log("GOING RIGHT");
    //    }
    //    if (Keyboard.current.wKey.isPressed)
    //    {
    //        move = new Vector2(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime);
    //        Debug.Log("GOING UP");
    //    }
    //    if (Keyboard.current.sKey.isPressed)
    //    {
    //        move = new Vector2(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime);
    //        Debug.Log("GOING DOWN");
    //    }

    //    this.transform.position = move;
    //}
    #endregion
}
