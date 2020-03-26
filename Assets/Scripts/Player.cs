using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;
    public float acceleration = 0.05f, deceleration = 0.05f;
    public float dashDuration = 0.5f, dashSpeed = 7.5f;

    public int maxAirJumps = 0;

    private int airJumps;

    private Vector2 dashDirection;

    private float movement = 0f, dashTimer = 0f;
    private bool onGround = true;

    private RaycastHit2D raycastHitLeft, raycastHitRight;

    private Rigidbody2D rb;
    private float rbGravity = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbGravity = rb.gravityScale;

        airJumps = maxAirJumps;
    }

    private void Update()
    {
        #region Ground Detection
        raycastHitRight = Physics2D.Raycast((Vector2)transform.position + new Vector2(0.5f, -0.51f), Vector2.down, 0.1f);
        raycastHitLeft = Physics2D.Raycast((Vector2)transform.position - Vector2.one * 0.51f, Vector2.down, 0.1f);
        onGround = (raycastHitRight.collider != null || raycastHitLeft.collider != null);

        if(onGround)
        {
            airJumps = maxAirJumps;
        }

        #endregion

        #region Movement
        if (dashTimer <= 0f)
        {
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
        }
        #endregion

        #region Jumping
        if (dashTimer <= 0f)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (onGround)
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    airJumps--;
                    print("jump");
                }
                else if (airJumps > 0)
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    airJumps--;
                }
            }
        }
        #endregion

        #region Dash
        dashTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Dash"))
        {
            if (dashTimer <= 0f && !onGround)
            {
                dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                if (!dashDirection.Equals(Vector2.zero))
                {
                    dashTimer = dashDuration;
                    rb.velocity = Vector2.zero;
                }
            }
        }
        #endregion

        rb.velocity = (dashTimer > 0f)
        ? dashDirection * dashSpeed
        : new Vector2(movement * moveSpeed * Time.deltaTime, rb.velocity.y);
    }
}
