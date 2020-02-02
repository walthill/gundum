using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemySpawner : MonoBehaviour
{
    [SerializeField] bool fastFirstSpawn;
    [SerializeField] float spawnTimer = 15;
    [SerializeField] GameObject enemySpritePrefab;

    GameObject enemyObj;
    float time;
    BoxCollider2D col;
    void Awake()
    {

        col = GetComponent<BoxCollider2D>();
       
        if (!fastFirstSpawn)
            time = spawnTimer;
        else
            time = 0;
    }

    internal void SpawnNewObject()
    {
        enemyObj = null;
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

            if (enemyObj == null)
                enemyObj = Instantiate(enemySpritePrefab, pos, transform.rotation, gameObject.transform) as GameObject;
        }
    }
}
