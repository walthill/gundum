using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] Camera shakyCamera;

    private bool shouldFollow;
    float camShakeValue = 0;

    //TODO: screen shake
    float shake;
    float maxAngle = 10, maxOffset = 5;
    Camera mainCam;
        
    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position += AsymptoticAverageLerp(transform.position, desiredPos, .05f);
        transform.LookAt(target);

        if (Input.GetKeyDown(KeyCode.S))
            AddTrauma(.5f);

        CameraShake(camShakeValue);
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

    public void AddTrauma(float intensity)
    {
        camShakeValue = intensity;
    }

    void CameraShake(float intensity)
    {
        if(intensity > 0)
        {
            shake = Random.Range(0.001f, 1.0f);
            float angle = maxAngle * shake * Random.Range(-1, 1);
            float xOffset = maxOffset * shake * Random.Range(-1, 1);
            float yOffset = maxOffset * shake * Random.Range(-1, 1);

            //Translational shake - currently doesn't work
            Vector2 foo = mainCam.rect.center + new Vector2(xOffset, yOffset);
            shakyCamera.rect.center.Set(foo.x, foo.y);
            Debug.Log(shakyCamera.rect.center);
            mainCam.rect = shakyCamera.rect;
            
            //Rotational shake
            shakyCamera.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
            transform.localEulerAngles = shakyCamera.transform.localEulerAngles;
            shakyCamera.transform.localEulerAngles = Vector3.zero;
           
            camShakeValue -= Time.fixedDeltaTime;
        }
    }

    Vector3 AsymptoticAverageLerp(Vector3 originPos, Vector3 targetPos, float percentage)
    {
        //Each frame, we move percentage closer to the target
        //x += target-x * .1;
        return (targetPos - originPos) * percentage;
    }

}
