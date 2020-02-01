using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    playerAudioScr PAS;
    [SerializeField] float jumpForce = 5;
    [SerializeField] private float doubleJumpScale = 1.25f;
    [SerializeField] bool canDoubleJump;

    Rigidbody2D rb;
    bool jump, doubleJump;
    public bool grounded;
    Vector2 jumpVec;
    FollowCamera followCam;

    void Awake()
    {
        PAS = GetComponent<playerAudioScr>();
        followCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>();
        rb = GetComponent<Rigidbody2D>();
        jumpVec = new Vector2(0, jumpForce);
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        DoJump();
    }

    private void DoJump()
    {
        if(jump && grounded)
        {
            grounded = false;
            rb.velocity = jumpVec;
            PAS.PlaySoundByIndex(0);
        }
        else if(jump && !grounded)
        {
            if (canDoubleJump)
            {
                if (doubleJump)
                {
                    doubleJump = false;
                    rb.velocity += jumpVec * doubleJumpScale;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(!grounded)
            {
                grounded = true;
                doubleJump = true;
            }
        }
    }

    void CheckInput()
    {
        jump = Input.GetButtonDown("Jump");
    }
}
