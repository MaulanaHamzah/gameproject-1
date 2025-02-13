using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        
        // Jika ada input horizontal, atur kecepatan horizontal
        if (moveInput != 0)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // Balikkan sprite berdasarkan arah gerakan
            spriteRenderer.flipX = moveInput < 0;
        }
        else
        {
            // Jika tidak ada input, atur kecepatan horizontal menjadi nol
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Set parameter "Speed" di Animator
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Set parameter "IsJumping" di Animator
        animator.SetBool("IsJumping", !isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (Mathf.Abs(rb.velocity.x) < 0.01f)
            {
                rb.AddForce(new Vector2(0.1f * Mathf.Sign(rb.velocity.x), 0));
            }
        }
    }
}