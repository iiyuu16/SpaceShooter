using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileContoller : MonoBehaviour
{
    float speed;
    public GameObject Explosion;
    public GameObject Clash;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2 (position.x + speed * Time.deltaTime, position.y);
        transform.position = position;
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

        if(transform.position.x > max.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            PlayerStats.playerStats.UpdateScore(500);
        }

        if (other.tag == "enemyProjectile")
        {
            PlayerStats.playerStats.UpdateScore(100);
        }

        if (other.tag == "Boss")
        {
            PlayerStats.playerStats.UpdateScore(5000);
        }

        if (other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;
            gameController.PlayHit(gameController.hitSFX);
            Destroy(gameObject);
            Destroy(other.gameObject);

            GameObject clash = (GameObject)Instantiate(Clash);
            clash.transform.position = expos;
        }

        if (other.tag == "Enemy")
        {
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            Destroy (gameObject);
            Destroy (other.gameObject);

            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }
    }
}