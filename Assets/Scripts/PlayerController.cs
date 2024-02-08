using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;
    public float speed;
    public Animator anim;
    public GameObject Projectile;
    public GameObject projectilePosition;
    float fireInterval = .3f;
    float nextFire;
    public GameObject Explosion;
    public GameObject Clash;

    public bool isGameOver = false;
    public GameObject RespawnPoint;

    public GameController gameController;
    void Awake()
    {
        if(playerController == null)
        {
            playerController = this;
        }
        else if (playerController != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextFire = fireInterval;
    }

    // Update is called once per frame
    public void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
        Fire();
    }

    void Fire()
    {
        nextFire -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && nextFire <= 0)
        {
            GameObject projectile = (GameObject)Instantiate(Projectile);
            projectile.transform.position = projectilePosition.transform.position;
            nextFire = fireInterval;
            gameController.PlayShoot(gameController.shootSFX);
        }

    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //fixed reso
        min.x = -6.86f; 
        max.x = 6.86f;

/*      min.x = -10.2f;
        max.x = 10.2f;*/

        min.y = -4.4f;
        max.y = 4.4f;

        Vector2 pos = transform.position;
        
        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("up", 1);
        }
        else
        {
            anim.SetInteger("up", 0);
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("down", -1);
        }
        else
        {
            anim.SetInteger("down", 0);
        }

        transform.position = pos;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            PlayerStats.playerStats.playerLife--;

            Destroy(other.gameObject);
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }

        if (other.tag == "enemyProjectile")
        {
            PlayerStats.playerStats.playerLife--;

            Vector2 expos = transform.position;
            gameController.PlayHit(gameController.hitSFX);
            GameObject clash = (GameObject)Instantiate(Clash);
            clash.transform.position = expos;
        }

        if (other.tag == "Boss")
        {
            PlayerStats.playerStats.playerLife--;
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }

        if (PlayerStats.playerStats.playerLife > 0)
        {
            StartCoroutine(IFrameSprite(3f));
            StartCoroutine(IFrames(3f));
            transform.position = RespawnPoint.transform.position;
        }
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if(currentSceneName == "1P_SpaceShooter")
            {
                gameController.GameOver1();
            }
            else if(currentSceneName == "1P_BossLevel")
            {
                gameController.GameOver2();
            }
        }
    }

    private IEnumerator IFrameSprite (float seconds)
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        while (seconds > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            seconds -= 0.1f;

        }

        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }

    private IEnumerator IFrames(float seconds)
    {
        GetComponent<BoxCollider2D>().enabled = false;            
        Debug.Log("immune");
        yield return new WaitForSeconds(seconds);
        GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("not immune");
    }
}
