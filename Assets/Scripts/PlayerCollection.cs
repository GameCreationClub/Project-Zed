using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    public int amountOfCrystals = 0;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crystal"))
        {
            Destroy(collision.gameObject);
            amountOfCrystals++;
        }
        else if (collision.CompareTag("Powerup"))
        {
            Powerup powerup = collision.GetComponent<Powerup>();
            Destroy(collision.gameObject);

            switch (powerup.powerupType)
            {
                case GlobalEnums.PowerupType.Dash:
                    playerMovement.hasDashAbility = true;
                    break;
                case GlobalEnums.PowerupType.DoubleJump:
                    playerMovement.maxAirJumps = 1;
                    break;
                case GlobalEnums.PowerupType.GravityFlip:
                    //Gravity flip code
                    print("Gravity flip");
                    break;
            }
        }
    }
}
