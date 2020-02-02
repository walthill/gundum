using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapDropper : MonoBehaviour
{
    [SerializeField] GameObject scrapPrefab;

    public void DropScrapLoot()
    {
        GameObject scrapLoot = Instantiate(scrapPrefab, transform.position, transform.rotation) as GameObject;
    }
}
