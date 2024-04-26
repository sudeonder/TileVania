using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    Vector2 moveInput;
    float gravityAtStart;
    Rigidbody2D rb;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Animator animator;
    bool isAlive;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = rb.gravityScale;

        isAlive = true;
    }

    
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // how does it reset??
    }

    void OnJump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}
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

        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        { 
            rb.gravityScale = gravityAtStart;
            return;
        }

        rb.gravityScale = 0;

        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        // set animation state to climbing
        animator.SetBool("isClimbing", moveInput.y != 0);
        rb.velocity = climbVelocity;
    }

    void Die()
    {
        // if collision with Enemy layer
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            animator.SetBool("isDeath", true);
            rb.velocity = deathKick;
        }

    }

   
}
