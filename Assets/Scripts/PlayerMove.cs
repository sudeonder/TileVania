using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float jumpSpeed;
    Vector2 moveInput;
    float gravityAtStart;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityAtStart = rb.gravityScale;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // how does it reset??
    }

    void OnJump()
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}
        rb.velocity += Vector2.up * jumpSpeed;
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        
        // set animation state to running
        animator.SetBool("isRunning", moveInput.x != 0);

    }

    void FlipSprite()
    {
        // only flip if there is movement
        if (moveInput.x == 0) return;
        transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
    }

    void ClimbLadder()
    {

        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        { 
            rb.gravityScale = gravityAtStart;
            return;
        }

        rb.gravityScale = 0;

        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
    }

   
}
