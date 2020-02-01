using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScapBox : MonoBehaviour
{
    [SerializeField] int scrapValue = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().PickUpScrap(scrapValue);
            Destroy(gameObject);
        }
    }

    public void SetScrapValue(int amt) { scrapValue = amt; }
}
