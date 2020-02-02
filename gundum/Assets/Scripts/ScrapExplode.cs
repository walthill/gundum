using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapExplode : MonoBehaviour
{
    [SerializeField] List<Rigidbody2D> rbList;
    [SerializeField] float explosionForce = 10;
    //[SerializeField] AudioClip collectSound;
    //AudioSource csources;

    private void Start()
    {
        //csources = gameObject.AddComponent<AudioSource>();
        //csources.clip = collectSound;
    }
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
