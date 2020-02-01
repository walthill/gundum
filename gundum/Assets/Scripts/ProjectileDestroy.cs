using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyRoutine());
    }

    //Big shoutout to Gabe Troyan
    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(timeToDestroy);

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
