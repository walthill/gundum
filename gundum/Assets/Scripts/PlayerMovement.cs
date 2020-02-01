using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3, maxVelocity = 3;

    Rigidbody2D rb;
    float xMove;
    Vector2 movement = Vector2.zero;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();
        Move();
    }

    void Move()
    {
        if (xMove != 0)
        {
            movement = new Vector2(xMove * moveSpeed, 0);

            if(Mathf.Abs(rb.velocity.x) < maxVelocity)
                rb.AddForce(movement);
        }
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("LeftStickX");
    }
}
