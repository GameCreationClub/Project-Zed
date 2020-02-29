using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;

    private float movement;
    private bool onGround = true;

    private RaycastHit2D raycastHitLeft, raycastHitRight;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        #region Movement
        movement = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * movement * moveSpeed * Time.deltaTime);
        #endregion

        #region Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
        #endregion

        #region Ground Detection
        raycastHitRight = Physics2D.Raycast((Vector2)transform.position + new Vector2(0.5f, -0.51f), Vector2.down, 0.6f);
        raycastHitLeft = Physics2D.Raycast((Vector2)transform.position - Vector2.one * 0.51f, Vector2.down, 0.6f);

        try
        {
            onGround = raycastHitRight.collider.CompareTag("Ground") || raycastHitLeft.collider.CompareTag("Ground");
        }
        catch
        {
            onGround = false;
        }
        #endregion
    }
}
