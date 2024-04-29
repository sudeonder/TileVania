using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Vector2 bulletVelocity = new Vector2(5f, 0.0f);
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        rigidBody.velocity = bulletVelocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // destroy enemy
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // if bullet collides with anything - destroy bullet
        Destroy(gameObject);
    }
}
