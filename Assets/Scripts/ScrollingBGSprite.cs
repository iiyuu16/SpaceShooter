using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBGSprite : MonoBehaviour
{

    public float scrollSpeed;
    float offset;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.time * scrollSpeed;
        rend.size = new Vector2(2.0f + offset, 1);
        //rend.sprite.mainTextureOffset = new Vector2(0, offset);
    }
}
