using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ShootingData
{
    public GameObject bulletPrefab;
    public Transform target;
    public float bulletSpeed, fireRate, shotRadius, waitTime;
}