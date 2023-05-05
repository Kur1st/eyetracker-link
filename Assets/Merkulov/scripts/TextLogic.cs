using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLogic : MonoBehaviour
{
    public float time = 2;
    public float speed = 2;
    public string value;
    public TMP_Text textSample;
    // Start is called before the first frame update
    void Start()
    {
        textSample.alpha = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        textSample.text = value;
        if (time < 0)
            Destroy(gameObject);
        textSample.alpha -= Time.deltaTime / time / 2;
        Vector3 tmp = transform.position;
        tmp.y += speed * Time.deltaTime;
        time -= Time.deltaTime;
        transform.position = tmp;
    }
}
