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

    float timeToBlow = 1;
    float time = 0;
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

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector3 wp = TobiiHelper.getWorldPoint();

        if ((wp - pos).magnitude <= 1.5f)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }
        if (time >= timeToBlow)
            OnMouseOver();
    }
}

