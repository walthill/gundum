using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;
        gameObject.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = target.position;
    }
}
