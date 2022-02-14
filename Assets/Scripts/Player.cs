using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private const float SPEED = 5f;

    private Controls controls;

    private void Start()
    {
        this.controls = InputManager.Instance.GetControls();
        this.controls.Player.Enable();
        //Comment out if you'll use MoveByRB
        //this.controls.Player.Move.Disable();
    }

    private void Update()
    {
        MoveByRB();
    }

    private void MoveByTransform()
    {
        Vector2 move = this.transform.position;

        if (this.controls.Player.MoveUp.IsPressed())
            move = this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + SPEED * Time.deltaTime);
        else if (this.controls.Player.MoveDown.IsPressed())
            move = this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - SPEED * Time.deltaTime);
        else if (this.controls.Player.MoveLeft.IsPressed())
            move = this.transform.position = new Vector2(this.transform.position.x - SPEED * Time.deltaTime, this.transform.position.y);
        else if (this.controls.Player.MoveRight.IsPressed())
            move = this.transform.position = new Vector2(this.transform.position.x + SPEED * Time.deltaTime, this.transform.position.y);

        this.transform.position = move;
    }

    private void MoveByRB()
    {
        Vector2 move = this.controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = move * 5f;
    }
}
