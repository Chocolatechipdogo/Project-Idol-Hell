using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private bool isGrounded = false;
    private Rigidbody2D rb;


    public bool sidecrollingMode;

    public int playerHealth;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!sidecrollingMode)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");
            float otherMoveInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(moveInput * speed, otherMoveInput * speed);
        }
        else
        {
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("NOT");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        // Debug.Log(other.name);
        // ParticleSystemCollision.LifetimeLoss = 1;
        var pS = other.GetComponent<ParticleSystem>();
        //int numCollisionEvents = pS.GetCollisionEvents(other, collisionEvents);
        var collision = pS.collision;
        collision.lifetimeLoss = 1; 
       
    }
}