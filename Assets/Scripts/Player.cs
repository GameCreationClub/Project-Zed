using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;
    public float acceleration = 0.05f, deceleration = 0.05f;

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
        #region Ground Detection
        raycastHitRight = Physics2D.Raycast((Vector2)transform.position + new Vector2(0.5f, -0.51f), Vector2.down, 0.1f);
        raycastHitLeft = Physics2D.Raycast((Vector2)transform.position - Vector2.one * 0.51f, Vector2.down, 0.1f);
        onGround = (raycastHitRight.collider != null || raycastHitLeft.collider != null);

        #endregion

        #region Movement
        movement += Input.GetAxisRaw("Horizontal") * acceleration;

        if (movement > 1f)
            movement = 1f;
        else if (movement < -1f)
            movement = -1f;

        transform.Translate(Vector2.right * movement * moveSpeed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") == 0f)
        {
            if (movement > deceleration / 2f)
                movement -= deceleration;
            else if (movement < -deceleration / 2f)
                movement += deceleration;

            if (Mathf.Abs(movement) <= deceleration && Mathf.Abs(movement) > 0f)
                movement = 0f;
        }
        #endregion

        #region Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
        #endregion
    }
}
