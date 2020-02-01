using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 0.2f, reloadTime = 0.45f;
    [SerializeField] int shotsPerClip = 6;

    private float aimDirectionY, aimDirectionX, shoot;
    float aimValue;
    bool canFire;
    private float offset = 90;
    Vector3 bulletDirection;
    private int shotsLeft;
    private bool reloading;

    private void Awake()
    {
        bulletDirection = -muzzle.right;
        shotsLeft = shotsPerClip;
    }

    void Update()
    {
        CheckInput();
        DoShoot();
        DoAim();
    }

    private void DoAim()
    {
        aimValue = Mathf.Atan2(aimDirectionX, aimDirectionY) * Mathf.Rad2Deg;

        //Debug.Log(aimValue);
        int angleNew = ((((int)(aimValue + offset)) / 45))*45;
        //Debug.Log(angleNew);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleNew));

        bulletDirection = -muzzle.right * (90/offset);
    }

    public void SetOffset(float offsetValue)
    {
        offset = offsetValue;
        //bulletDirection = -bulletDirection;
    }

    void DoShoot()
    {
        if (shoot == 1)
        {
            if (canFire && shotsLeft > 0)
            {
                reloading = false;
                shotsLeft--;
                canFire = false;

                GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
                bullet.GetComponent<Bullet>().Fire(bulletDirection);
            }
        }
        else if (shoot <= fireRate)
            canFire = true;

        if (shotsLeft <= 0 && !reloading)
        {
            StartCoroutine(ReloadRoutine(reloadTime));
        }
    }

    IEnumerator ReloadRoutine(float cooldownTime)
    {
        reloading = true;
        yield return new WaitForSeconds(cooldownTime);
        shotsLeft = shotsPerClip;
    }

    void CheckInput()
    {
        shoot = Input.GetAxis("ShootTrigger");
        aimDirectionX = Input.GetAxis("RightStickX");
        aimDirectionY = Input.GetAxis("RightStickY");
    }
}
