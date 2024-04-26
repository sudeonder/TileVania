using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 moveVelocity = new Vector2(moveSpeed, 0.0f);
        rb.velocity = moveVelocity;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed *= -1;
        // flip enemy sprite
        transform.localScale = new Vector3(Mathf.Sign(moveSpeed), 1, 1);
    }
}
