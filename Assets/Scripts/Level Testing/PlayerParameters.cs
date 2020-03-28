using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParameters : MonoBehaviour
{
    public InputField moveSpeed, jumpForce, airJumpAmount, acceleration, deceleration, dashDuration, dashSpeed;
    public Toggle canDash;

    private PlayerMovement player;

    public void GetPlayer()
    {
        player = FindObjectOfType<PlayerMovement>();

        moveSpeed.text = player.moveSpeed.ToString();
        jumpForce.text = player.jumpForce.ToString();
        airJumpAmount.text = player.maxAirJumps.ToString();
        acceleration.text = player.acceleration.ToString();
        deceleration.text = player.deceleration.ToString();
        dashDuration.text = player.dashDuration.ToString();
        dashSpeed.text = player.dashSpeed.ToString();
        canDash.isOn = player.hasDashAbility;
    }

    public void ApplySettings()
    {
        try
        {
            player.moveSpeed = float.Parse(moveSpeed.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        try
        {
            player.jumpForce = float.Parse(jumpForce.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        try
        {
            player.maxAirJumps = int.Parse(airJumpAmount.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        try
        {
            player.acceleration = float.Parse(acceleration.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        try
        {
            player.deceleration = float.Parse(deceleration.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        try
        {
            player.dashDuration = float.Parse(dashDuration.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        try
        {
            player.dashSpeed = float.Parse(dashSpeed.text);
        }
        catch
        {
            Debug.LogError("Incorrect format.");
        }

        player.hasDashAbility = canDash.isOn;
    }
}
