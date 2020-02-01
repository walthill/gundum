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
    float shake = 0.5f;
    float maxAngle = 10, maxOffset = 3;
    Camera mainCam, shakyCamera;

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position += AsymptoticAverageLerp(transform.position, desiredPos, .05f);
        transform.LookAt(target);
    }



    //Camera shake
    // - trauma level based on outside factor. goes down at a fixed, linear rate. Damage/stress adds trauma
    // Camera shake is trauma^2 or ^3 
    //Translational + rotational shake
    // angle  = maxAngle * shake * GetRandomFloatNegOneToOne(); rotational
    // xOffset = maxOffset * shake * GetRandomFloatNegOneToOne(); translational
    // yOffset  = maxOffset * shake * GetRandomFloatNegOneToOne();

    // shakyCamera.angle = camera.angle + angle;
    // shakyCamera.center = camera.center + Vec2(offsetX, offsetY);

    //use perlin noise GetPerlinNoies(seed, time,...)
    void CameraShake()
    {
        float angle = maxAngle * shake * Mathf.PerlinNoise(0, Time.time);
        float xOffset = maxOffset * shake * Mathf.PerlinNoise(1, Time.time);
        float yOffset = maxOffset * shake * Mathf.PerlinNoise(2, Time.time);

        shakyCamera.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angle, transform.localEulerAngles.y + angle, transform.localEulerAngles.z + angle);
        Vector2 foo = mainCam.rect.center + new Vector2(xOffset, yOffset);
        shakyCamera.rect.center.Set(foo.x, foo.y);

        mainCam.rect = shakyCamera.rect;
        transform.localEulerAngles = shakyCamera.transform.localEulerAngles;
    }


    Vector3 AsymptoticAverageLerp(Vector3 originPos, Vector3 targetPos, float percentage)
    {
        //Each frame, we move percentage closer to the target
        //x += target-x * .1;
        return (targetPos - originPos) * percentage;
    }

}
