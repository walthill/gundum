﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int currentHealth = 100; //player can take 4 hits
    [SerializeField] int maxHealth = 100; //player can take 4 hits
    [SerializeField] int damageValue = 25;
    [SerializeField] int scrap = 0, maxScrap = 50;   //currency
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
        if (currentHealth > 0)
            currentHealth -= damageValue;
        else
            GameOver();
    }

    public void PickUpScrap(int amt) { scrap += amt; }

    public int SpendScrap(int amt)
    {
        if (scrap > 0)
        {
            scrap -= amt;
            return amt;
        }

        return 0;
    }

    public void LoadMissle() 
    {
        if(missles == 1)
            missles -= 1;
    }

    public bool PickUpMissile()
    {
        if (missles == 0)
        {
            missles += 1;
            return true;
        }

        return false;
    }

    void GameOver()
    {
        SceneManager.LoadScene("end");
    }
}
