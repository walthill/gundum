using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    playerAudioScr PAS;
    ShakeCamera SHAKE;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField]
    GameObject muzzleFlash;

    private float aimDirectionY, aimDirectionX, shoot;
    float aimValue;
    bool canFire;
    private float offset = 90;
    Vector3 bulletDirection;

    private void Awake()
    {
        bulletDirection = -muzzle.right;
        PAS = GetComponentInParent<playerAudioScr>();
        SHAKE = Camera.main.GetComponent<ShakeCamera>();
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
            if (canFire)
            {
                canFire = false;

                GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
                bullet.GetComponent<Bullet>().Fire(bulletDirection);
                //play sound
                PAS.PlaySoundByIndex(1);
                //do shake
                SHAKE.AddTrauma(.08f, .15f);
                //muzzleFlash
                muzzleFlash.SetActive(true);
                StartCoroutine(waitToMakeFlashDisapear(.1f));
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


    IEnumerator waitToMakeFlashDisapear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        muzzleFlash.SetActive(false);
    }
}
