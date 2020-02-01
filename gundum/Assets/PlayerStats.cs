using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int currentHealth = 100; //player can take 4 hits
    [SerializeField] int maxHealth = 100; //player can take 4 hits
    [SerializeField] int damageValue = 25;
    [SerializeField] int scrap = 0;   //currency
    [SerializeField] int missles = 1; //missle to load into weapon system. can only carry one

    public bool Heal()
    {
        bool hasHealed = false;

        if (currentHealth < maxHealth)
        {
            currentHealth += damageValue;
            hasHealed = true;
        }

        return hasHealed;
    }
    public void TakeDamage()
    { 
        if(currentHealth > 0)
            currentHealth -= damageValue;
        else
        {
            Debug.Log("GAME OVER");

        }

    }
    public void PickUpScrap(int amt) { scrap += amt; }
    public void SpendScrap(int amt) { scrap -= amt; }

    public void LoadMissle() 
    {
        if(missles == 1)
            missles -= 1;
    }

    public void PickUpMissle()
    {
        if (missles == 0)
            missles += 1;
    }
}
