using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FoodLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float camera_size = Camera.main.orthographicSize;
        float ratio = (float)Screen.height / Screen.width;
        float x = camera_size / ratio;
        float y = camera_size;
        Vector3 randMoving = new (Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0);
        transform.position += randMoving;
        
        if (Vector2.Distance(transform.position, Camera.main.transform.position) > (x + y) * 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
