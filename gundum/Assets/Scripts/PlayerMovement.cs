using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3, maxVelocity = 3;
    [SerializeField] Shoot gunScript;

    Rigidbody2D rb;
    bool movingRight = false;
    float xMove;
    Vector2 movement = Vector2.zero;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();
        Move();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < maxVelocity)
            rb.AddForce(movement);
    }

    void Move()
    {
        if (xMove != 0)
        {
            movement = new Vector2(xMove * moveSpeed, 0);
        }

        if (xMove < -0.5f)
        {
            if (movingRight)
            {
                gunScript.SetOffset(90);
                movingRight = false;
                FlipSprite();
            }
        }
        else if (xMove > 0.5f)
        {
            if(!movingRight)
            {
                gunScript.SetOffset(-90);
                movingRight = true;
                FlipSprite();
            }
        }
    }

    void FlipSprite()
    {
        float x = -1 * transform.localScale.x;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("LeftStickX");
    }
}
