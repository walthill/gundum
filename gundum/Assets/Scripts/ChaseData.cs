using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ChaseData
{
    public Transform target;
    public float chaseRadius, chaseSpeed, waitTime;
}