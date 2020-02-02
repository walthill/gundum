using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCollection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            int scrapAmount = Random.Range(1, 2);
            collision.gameObject.GetComponent<PlayerStats>().PickUpScrap(scrapAmount);
            Destroy(gameObject);
        }
    }
}
