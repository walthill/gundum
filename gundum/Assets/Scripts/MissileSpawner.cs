using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float spawnTimer = 20;

    GameObject missile;

    float time;
    BoxCollider2D col;
    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        time = spawnTimer;
    }

    internal void SpawnNewObject()
    {
        missile = null;
        time = spawnTimer;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            float xPos = Random.Range(col.bounds.min.x, col.bounds.max.x);
            float yPos = Random.Range(col.bounds.min.y, col.bounds.max.y);

            Vector2 pos = new Vector2(xPos, yPos);
            if(missile == null)
                missile = Instantiate(missilePrefab, pos, transform.rotation, gameObject.transform) as GameObject;
        }
    }
}
