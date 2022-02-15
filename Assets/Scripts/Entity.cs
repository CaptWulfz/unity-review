using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    protected float speed = 5f;

    Controls controls;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.controls = new Controls();
        this.controls.Player.Enable();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveByRigidBody();
    }

    protected void MoveByRigidBody()
    {
        Vector2 move = this.controls.Player.Movement.ReadValue<Vector2>();
        this.rb.velocity = move * speed;
    }

    protected void MoveByTransform()
    {
        Vector2 move = this.transform.position;
        // Moving Left
        if (Keyboard.current.aKey.isPressed)
        {
            move = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y);
        }

        //Moving Right
        if (Keyboard.current.dKey.isPressed)
        {
            move = new Vector2(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y);
        }

        // Moving Down
        if (Keyboard.current.sKey.isPressed)
        {
            move = new Vector2(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime);
        }

        // Moving Up
        if (this.controls.Player.MoveUp.IsPressed())
        {
            move = new Vector2(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime);
        }

        this.transform.position = move;
    }
}
