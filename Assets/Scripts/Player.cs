using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;

    private float movement;
    private bool onGround = true;

    private RaycastHit2D hit2D;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }

        hit2D = Physics2D.Raycast((Vector2)transform.position + Vector2.down * 0.51f, Vector2.down, 0.6f);

        try
        {
            onGround = hit2D.collider.CompareTag("Ground");
        }
        catch
        {
            onGround = false;
        }
    }
}
