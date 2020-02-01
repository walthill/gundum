using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 0.2f;

    private float aimDirectionY, aimDirectionX, shoot;
    float aimValue;
    bool canFire;
    private float offset = 90;
    Vector3 bulletDirection;

    private void Awake()
    {
        bulletDirection = -muzzle.right;
    }

    void Update()
    {
        CheckInput();
        DoShoot();
        DoAim();
    }

    private void DoAim()
    {
       // aimValue = Mathf.Atan2(aimDirectionX, aimDirectionY) * Mathf.Rad2Deg;
     //   transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, aimValue + offset));
    }

    public void SetOffset(float offsetValue)
    {
        offset = offsetValue;
        bulletDirection = -bulletDirection;
    }

    void DoShoot()
    {
        if (shoot == 1)
        {
            if (canFire)
            {
                canFire = false;

                GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
                bullet.GetComponent<Bullet>().Fire(bulletDirection);
            }
        }
        else if (shoot <= fireRate)
            canFire = true;
    }

    void CheckInput()
    {
        shoot = Input.GetAxis("ShootTrigger");
        aimDirectionX = Input.GetAxis("RightStickX");
        aimDirectionY = Input.GetAxis("RightStickY");
    }

}
