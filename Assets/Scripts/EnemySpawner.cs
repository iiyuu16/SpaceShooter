using System.Collections;
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
    public float difficultyMult = -1f;

    private float timer = 0f;
    private float difficultyIncreaseInterval = 8f; // Increase difficulty every x seconds

    void Awake()
    {
        if (enemySpawner == null)
        {
            enemySpawner = this;
        }
        else if (enemySpawner != this)
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
        timer += Time.deltaTime;

        if (timer >= difficultyIncreaseInterval)
        {
            difficultyMult += 0.1f;
            timer = 0f;
            Debug.Log("diff: "+ difficultyMult);
        }

        if (spawnInterval <= difficultyMult)
        {
            float spawnXPosition = Random.Range(xPosMin, xPosMax);
            float spawnYPosition = Random.Range(yPosMin, yPosMax);

            GameObject enemyShip = Instantiate(enemyPrefab);
            enemyShip.transform.position = new Vector2(spawnXPosition, spawnYPosition);

            spawnInterval = Random.Range(1, 3);
        }
    }
}
