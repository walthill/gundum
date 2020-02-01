using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointDisable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Joint")
        {
            col.isTrigger = true;
            col.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Joint")
        {
            col.isTrigger = false;
            col.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
