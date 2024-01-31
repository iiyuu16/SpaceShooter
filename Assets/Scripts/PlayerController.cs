using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;
    public float speed;
    public Animator anim;
    public GameObject Projectile;
    public GameObject projectilePosition;
    float fireInterval = .25f;
    float nextFire;

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
    void Update()
    {
        nextFire -= Time.deltaTime;
        if(Input.GetKey(KeyCode.Space) && nextFire <= 0) {
            GameObject projectile = (GameObject)Instantiate (Projectile);
            projectile.transform.position = projectilePosition.transform.position;
            nextFire = fireInterval;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - .5f;
        min.x = min.x + .5f;

        max.y = max.y - .5f;
        min.y = min.y + .5f;

        Vector2 pos = transform.position;
        
        pos += direction * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            anim.SetInteger("up", 1);
        }
        else
        {
            anim.SetInteger("up", 0);
        }

        
        if(Input.GetKey(KeyCode.S))
        {
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            anim.SetInteger("down", -1);
        }
        else
        {
            anim.SetInteger("down", 0);
        }

        transform.position = pos;

    }
}
