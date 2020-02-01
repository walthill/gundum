using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;

    private float aimDirectionY, aimDirectionX, shoot;
    float aimValue;

    void Update()
    {
        CheckInput();

        if (shoot == 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
            bullet.GetComponent<Bullet>().Fire(muzzle.transform.right);
        }


        aimValue = Mathf.Atan2(aimDirectionX, aimDirectionY) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, aimValue);
    }

    void CheckInput()
    {
        shoot = Input.GetAxis("ShootTrigger");
        aimDirectionX = Input.GetAxis("RightStickX");
        aimDirectionY = Input.GetAxis("RightStickY");
    }

}
