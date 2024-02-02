using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContoller : MonoBehaviour
{
    float speed;
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
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
            PlayerStats.playerStats.UpdateScore();
        }

        if(other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;
            Destroy (gameObject);
            Destroy (other.gameObject);

            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;

            }
        }
    }
