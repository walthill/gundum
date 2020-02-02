using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyHealth : MonoBehaviour
{
    [SerializeField] int currentHealth, maxHealth, damageValue;

   
    public void TakeDamage()
    {
        if (currentHealth > 0)
            currentHealth -= damageValue;

        if (currentHealth <= 0)
        {
            GetComponentInParent<ChaseEnemySpawner>().SpawnNewObject();
            GetComponent<ScrapDropper>().DropScrapLoot();
            Destroy(gameObject); // destroy this enemy
        }
    }
}
