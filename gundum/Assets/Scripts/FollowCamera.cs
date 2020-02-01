using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset = Vector3.zero;
    private bool shouldFollow;

    //TODO: screen shake

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position += AsymptoticAverageLerp(transform.position, desiredPos, .05f);
        transform.LookAt(target);

        transform.rotation =  Quaternion.identity;
    }

    //Each frame, we move percentage closer to the target
    //x += target-x * .1;
    Vector3 AsymptoticAverageLerp(Vector3 originPos, Vector3 targetPos, float percentage)
    {
        return (targetPos - originPos) * percentage;
    }

}
