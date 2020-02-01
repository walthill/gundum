using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy = 5.0f;

    float newTimeToDestroy;

    void Start()
    {
        StartCoroutine(DestroyRoutine(timeToDestroy));
    }

    public void DestroyBullet()
    {
        StartCoroutine(DestroyRoutine(0.0f));
    }

    //Big shoutout to Gabe Troyan
    IEnumerator DestroyRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Vector3 targetScale = new Vector3(0.005f, 0.005f, 0.005f);

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            gameObject.transform.localScale = Vector3.Lerp(transform.localScale, targetScale, i * 0.5f);
            yield return null;
        }

        gameObject.transform.localScale = targetScale;
        Destroy(gameObject);
    }
}
