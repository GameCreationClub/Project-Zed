using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private LevelTransition levelTransition;
    private PlayerMovement playerMovement;

    private void Start()
    {
        levelTransition = FindObjectOfType<LevelTransition>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            if (levelTransition != null)
            {
                playerMovement.Stop();
                levelTransition.NextLevel();
            }
        }
    }
}
