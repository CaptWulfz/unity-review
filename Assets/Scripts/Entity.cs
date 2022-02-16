using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Entity : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected AudioSource source;


    protected string sourceName = "";
    protected float speed = 5f;
    
    
    Controls controls;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // "this" is for global variables
        this.controls = new Controls();
        this.controls.Player.Enable();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //MoveByRigidBody();
    }

    protected void MoveByRigidBody()
    {
        // Read Value 
        // <> is for type
        Vector2 move = this.controls.Player.Movement.ReadValue<Vector2>();
        this.rb.velocity = move * speed;
        Debug.Log("Moving through" + move);
    }

    protected void MoveByTransform()
    {
        Vector2 move = this.transform.position;
        // Input.GetKeyUp(KeyCode.A)
        // Moving Left
        // deltaTime is based when
        if (Keyboard.current.aKey.isPressed)
        {
            move = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y);
            Debug.Log("GOING LEFT");
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move = new Vector2(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y);
            Debug.Log("GOING RIGHT");
        }
        if (Keyboard.current.wKey.isPressed)
        {
            move = new Vector2(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime);
            Debug.Log("GOING UP");
        }
        if (Keyboard.current.sKey.isPressed)
        {
            move = new Vector2(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime);
            Debug.Log("GOING DOWN");
        }

        this.transform.position = move;
    }
}
