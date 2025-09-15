using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;
    public Rigidbody2D RB;
    public InputActionReference move;
    private Vector2 moveInputVector;

    void Start()
    {
        move.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputVector = move.action.ReadValue<Vector2>();
        Turn();
    }

    void FixedUpdate()
    {
        RB.linearVelocity = new Vector2(moveInputVector.x * speed, moveInputVector.y * speed);
    }

    void Turn()
    {
        if (RB.linearVelocity != Vector2.zero)
        {
            if(moveInputVector.x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if(moveInputVector.x < 0)
            {
                transform.localScale = new Vector3((Mathf.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
            }
        }
    }

}