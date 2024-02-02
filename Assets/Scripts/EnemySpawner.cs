using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public static EnemySpawner enemySpawner;

    public float xPosMax = 7f;
    public float xPosMin = 5f;
    public float yPosMax = 2.75f;
    public float yPosMin = -2.75f;
    float spawnInterval;
    private int currentEnemies = 0;


    void Awake()
    {
        if(enemySpawner == null)
        {
            enemySpawner = this;
        }    
        else if(enemySpawner != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnInterval = Random.Range(1, 3);
    }

    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if(spawnInterval <= 0)
        {
            float spawnXPosition = Random.Range(xPosMin, xPosMax);
            float spawnYPosition = Random.Range(yPosMin, yPosMax);

            GameObject enemyShip = (GameObject)Instantiate(enemyPrefab);
            enemyShip.transform.position = new Vector2(spawnXPosition, spawnYPosition);
        
            spawnInterval = Random.Range(1, 3);
        }
    }
}
