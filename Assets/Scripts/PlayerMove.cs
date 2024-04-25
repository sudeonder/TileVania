using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        // set animation state to running
        animator.SetBool("isRunning", moveInput.x != 0);

    }

    void FlipSprite()
    {
        // only flip if there is movement
        if (moveInput.x == 0) return;
        transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
    }
}
