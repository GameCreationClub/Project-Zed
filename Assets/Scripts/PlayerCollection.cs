using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    public int amountOfCrystals = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Crystal"))
        {
            Destroy(collision.gameObject);
            amountOfCrystals++;
        }
    }
}
