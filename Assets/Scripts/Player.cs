﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 390f;
    public float acceleration = 0.05f, deceleration = 0.05f;
    public float dashDuration = 0.5f, dashSpeed = 7.5f;

    private float movement = 0f, dashTimer = 0f, dashDirection = 1f;
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

            if (Input.GetAxisRaw("Horizontal") != 0f)
                dashDirection = Input.GetAxisRaw("Horizontal");
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
                }
            }
        }
        #endregion

        #region Dash
        dashTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Dash"))
        {
            if (dashTimer <= 0f)
            {
                dashTimer = dashDuration;
            }
        }

        if (dashTimer > 0f)
        {
            transform.Translate(Vector2.right * dashDirection * dashSpeed * Time.deltaTime);
        }
        #endregion
    }
}
