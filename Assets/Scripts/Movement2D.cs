using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Movement2D : MonoBehaviour
{   
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float linearDrag;
    private float horizontalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x <0f && horizontalDirection > 0f);
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       horizontalDirection = GetInput().x;
       if (horizontalDirection <0f && facingRight)
       {
        Flip();
       }
       else if(horizontalDirection > 0f && !facingRight)
       {
        Flip();
       }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        ApplyLinearDrag();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(horizontalDirection,0f) * movementAcceleration);

        if(Math.Abs(rb.velocity.x) > maxMoveSpeed) 
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
    }

    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }
}
