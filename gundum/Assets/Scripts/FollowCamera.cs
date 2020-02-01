using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Camera Follow Values")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float smoothingPercentage = 0.05f;

    void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position += AsymptoticAverageLerp(transform.position, desiredPos, smoothingPercentage);
        transform.LookAt(target);

        transform.rotation = Quaternion.identity;
    }

    Vector3 AsymptoticAverageLerp(Vector3 originPos, Vector3 targetPos, float percentage)
    {
        //Each frame, we move percentage closer to the target
        //x += target-x * .1;
        return (targetPos - originPos) * percentage;
    }
}
