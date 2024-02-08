using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float initialFiringRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float firingRate;

    void Start()
    {
        firingRate = initialFiringRate;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<EnemyProjectile>().speed = speed;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    // Adjust the firing rate dynamically
    public void SetFiringRate(float newFiringRate)
    {
        firingRate = newFiringRate;
    }
}
