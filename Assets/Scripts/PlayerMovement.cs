using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;
    public float acceleration = 0.05f, deceleration = 0.05f;
    public float dashDuration = 0.5f, dashSpeed = 7.5f;

    public bool hasDashAbility = true;

    public int maxAirJumps = 0;

    public Transform visionMask;

    private bool canDash;
    private int airJumps;

    private Vector2 dashDirection;

    private float movement = 0f, dashTimer = 0f, movementMultiplier = 180f;
    private bool onGround = true;

    private bool justFinishedDash = false;

    private RaycastHit2D raycastHitLeft, raycastHitRight;

    private Rigidbody2D rb;
    private float retainXVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        airJumps = maxAirJumps;

        visionMask = transform.GetChild(0);
    }

    private void Update()
    {
        #region Ground Detection
        raycastHitRight = Physics2D.Raycast((Vector2)transform.position + new Vector2(0.5f, -0.51f), Vector2.down, 0.05f);
        raycastHitLeft = Physics2D.Raycast((Vector2)transform.position - Vector2.one * 0.51f, Vector2.down, 0.05f);
        onGround = (raycastHitRight.collider != null || raycastHitLeft.collider != null);

        #endregion

        #region Movement
        if (dashTimer <= 0f)
        {
            movement += Input.GetAxisRaw("Horizontal") * acceleration;

            if (movement > 1f)
                movement = 1f;
            else if (movement < -1f)
                movement = -1f;

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
                    retainXVelocity = movement;

                    onGround = false;
                    rb.AddForce(Vector2.up * jumpForce);
                    rb.velocity = new Vector2(retainXVelocity, rb.velocity.y);
                    airJumps--;
                }
                else if (airJumps >= 0)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * jumpForce);
                    airJumps--;
                }
            }
        }
        #endregion

        #region Dash
        if (hasDashAbility)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer < 0f)
            {
                if (justFinishedDash)
                {
                    rb.velocity = Vector2.zero;
                    justFinishedDash = false;
                }

                if (Input.GetButtonDown("Dash"))
                {
                    if (!onGround && canDash)
                    {
                        float directionalDashSpeed = 1f / Mathf.Sqrt(Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));
                        dashDirection = new Vector2(directionalDashSpeed * Input.GetAxisRaw("Horizontal"), directionalDashSpeed * Input.GetAxisRaw("Vertical"));

                        bool validDash = !(float.IsInfinity(directionalDashSpeed) || float.IsNaN(directionalDashSpeed));

                        if (validDash)
                        {
                            retainXVelocity = Input.GetAxisRaw("Horizontal");

                            dashTimer = dashDuration;
                            rb.velocity = Vector2.zero;
                            canDash = false;
                            justFinishedDash = true;
                        }
                    }
                }
            }
        }
        #endregion

        rb.velocity = (dashTimer > 0f)
        ? dashDirection * dashSpeed
        : new Vector2(movement * moveSpeed * movementMultiplier * Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onGround)
        {
            canDash = true;
            airJumps = maxAirJumps;
        }
    }

    public void SetVision(float vision)
    {
        visionMask.localScale = Vector3.one * vision;
    }
}
