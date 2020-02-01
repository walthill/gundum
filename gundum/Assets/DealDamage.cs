using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] bool destroySelf;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage();

            if(destroySelf)
                Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
