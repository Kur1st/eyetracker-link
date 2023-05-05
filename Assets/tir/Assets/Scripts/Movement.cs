using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Vector2 direction;
    public GameObject ballon;
    public Sprite newSprite;
    public Sprite[] balloons;
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
        Destroy(gameObject, 0.5f);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction);
    }

}

