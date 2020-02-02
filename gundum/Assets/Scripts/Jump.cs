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
    bool jump, jumpReleased, doubleJump = false;
    public bool grounded;
    Vector2 jumpVec;
    FollowCamera followCam;
    private float lowJumpMultiplier = 2f;

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
        if (jump && !grounded && canDoubleJump)
        {
            if (doubleJump)
            {
                doubleJump = false;
                rb.AddForce(jumpVec, ForceMode2D.Impulse);
            }
        }

        if (jump && grounded)
        {
            rb.AddForce(jumpVec, ForceMode2D.Impulse);
            grounded = false;
        }

        // Dynamic jumping - https://www.reddit.com/r/Unity3D/comments/26p2yk/variable_jump_height_depending_on_button_push/
        if (jumpReleased && !grounded)
        {
            if (rb.velocity.y > lowJumpMultiplier)
                rb.velocity = new Vector2(rb.velocity.x, lowJumpMultiplier);
        }

        /*
         * OLD
         * if(jump && grounded)
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
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" || collision.collider.tag == "Joint")
        {
            
            if (!grounded)
            {
                grounded = true;
                doubleJump = true;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    void CheckInput()
    {
        jumpReleased = Input.GetButtonUp("Jump");
        jump = Input.GetButtonDown("Jump");
    }
}
