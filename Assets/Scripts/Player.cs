using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;

    private float movement;
    private bool onGround = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * moveSpeed * Time.deltaTime, rb.velocity.y);
        //transform.Translate(Vector2.right * movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
