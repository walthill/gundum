using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapExplode : MonoBehaviour
{
    [SerializeField] List<Rigidbody2D> rbList;
    [SerializeField] float explosionForce = 10;

    void Awake()
    {
        foreach (var item in rbList)
            item.AddForce(Vector3.up * explosionForce);

        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(90);

        foreach (Transform child in transform)
            Destroy(gameObject);
    }
}
