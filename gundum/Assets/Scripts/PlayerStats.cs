﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int currentHealth = 100; //player can take 4 hits
    [SerializeField] int maxHealth = 100; //player can take 4 hits
    [SerializeField] int damageValue = 5;
    [SerializeField] int scrap = 0, maxScrap = 50;   //currency
    [SerializeField] int missles = 1; //missle to load into weapon system. can only carry one
    public Text healthText, scrapText, missilesText;
    ShakeCamera SHAKE;
    int healthBoostValue = 10;

    private void Start()
    {
        SHAKE = Camera.main.GetComponent<ShakeCamera>();
        UpdateUI();
    }

    public bool Heal()
    {
        bool hasHealed = false;

        if (currentHealth < maxHealth)
        {
            currentHealth += healthBoostValue;
            hasHealed = true;
        }

        UpdateUI();
        return hasHealed;
    }
    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            StartCoroutine(HitRoutine());
            currentHealth -= damageValue;
            UpdateUI();
            SHAKE.AddTrauma(.05f, .05f);
        }
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void PickUpScrap(int amt)
    {
        if (scrap < maxScrap)
        {
            scrap += amt;
            UpdateUI();
        }
    }

    public int SpendScrap(int amt)
    {
        if (scrap > 0)
        {
            scrap -= amt;
            UpdateUI();
            return amt;
        }

        return 0;
    }

    public void LoadMissle() 
    {
        if(missles == 1)
            missles -= 1;
        UpdateUI();
    }

    public bool PickUpMissile()
    {
        if (missles == 0)
        {
            missles += 1;
            UpdateUI();
            return true;
        }

        return false;
    }

    void GameOver()
    {
        SceneManager.LoadScene("end");
    }

    void UpdateUI()
    {
        healthText.text = currentHealth.ToString() + "/100";
        scrapText.text = scrap.ToString() + "/" + maxScrap.ToString();
        missilesText.text = missles.ToString() + "/1";

    }
    IEnumerator HitRoutine()
    {
        GetComponent<SpriteRenderer>().color = Color.red;  
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
