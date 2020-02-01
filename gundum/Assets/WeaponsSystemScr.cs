using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsSystemScr : MonoBehaviour
{
    [SerializeField]
    Color RocketReadyColor, RocketEmptyColor, WeNeedRocketDotCom;
    [SerializeField]
    List<Image> Rockets;
    [SerializeField]
    GameObject rocketBackGround;

    EnemyHealthScr enemyHealthScript;
    [SerializeField] private int totalAmmo = 3, missileTime = 5;
    int currentAmmo;

    void Start()
    {
        enemyHealthScript = GetComponent<EnemyHealthScr>();
        currentAmmo = totalAmmo;

        //function called, after x seconds, then repeats every y seconds
        InvokeRepeating("MechShootsMissile", missileTime, missileTime);
    }

    public void RestockAmmo(int amountRestocked)
    {
        if (currentAmmo == 0)
            InvokeRepeating("MechShootsMissile", missileTime, missileTime);

        currentAmmo += amountRestocked;
        if (currentAmmo > totalAmmo)
            currentAmmo = totalAmmo;
    }

    void MechShootsMissile()
    {
        if (currentAmmo == 0)
            CancelInvoke("MechShootsMissile");
        else
        {
            currentAmmo--;

            //damage enemy
            enemyHealthScript.RecieveDMG(Random.Range(50, 60));
        }
    }
}
