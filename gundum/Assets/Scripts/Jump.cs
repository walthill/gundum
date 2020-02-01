using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 5;

    Rigidbody2D rb;
    bool jump, grounded;
    Vector2 jumpVec;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpVec = new Vector2(0, jumpForce);
    }

    void Update()
    {
        CheckInput();
        DoJump();
    }

    private void DoJump()
    {
        if(jump && grounded)
        {
            rb.AddForce(jumpVec);
        }


    }

    void CheckInput()
    {
        jump = Input.GetButtonDown("Jump");
    }
}
