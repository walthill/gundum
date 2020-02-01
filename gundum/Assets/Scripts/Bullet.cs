using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public void Fire(Vector3 direction, float speed = int.MinValue)
    {
        if(speed == int.MinValue)
            rb.velocity = direction * bulletSpeed;
        else
            rb.velocity = direction * speed;
    }
}
