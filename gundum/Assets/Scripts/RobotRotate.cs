using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRotate : MonoBehaviour
{
    [SerializeField] private bool startClockwise;
    bool rotatingClockwise = true;
    [SerializeField] private float timeToRotate = 10;
    [SerializeField] private float rotationSpeed = 0.2f;

    void Start()
    {
        rotatingClockwise = startClockwise;
        InvokeRepeating("SwitchRotation", timeToRotate, timeToRotate);
    }

    void Update()
    {
        RotateLimb();
    }

    void RotateLimb()
    {
        if (rotatingClockwise)
            transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        else
            transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    void SwitchRotation()
    {
        rotatingClockwise = !rotatingClockwise;
    }
}
