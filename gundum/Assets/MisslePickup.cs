using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePickup : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().PickUpMissile();
            Destroy(gameObject);
        }
    }
}
