using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArmorLogic : MonoBehaviour
{
    public GameObject target;
    public bool is_good = true;
    public float angle = 0;
    private float axis_x, axis_y;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 tmp = target.transform.position;
        axis_x = target.transform.localScale.x * 1.05f;
        axis_y = target.transform.localScale.y * 1.05f;
        tmp.y += axis_y;
        transform.position = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        // Если пользователь получил урон, то is_good = false и красиво разлетаемся
        if (is_good)
        {
            angle += 0.5f;
            angle %= 360;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Vector3 tmp = target.transform.position;
            tmp.x += math.cos(angle * math.PI / 180) * axis_x;
            tmp.y += math.sin(angle * math.PI / 180) * axis_x;
            transform.position = tmp;
        }
        else
        {
            Vector3 tmp = transform.position;
            tmp.x += math.cos(angle * math.PI / 180) * axis_x / 4;
            tmp.y += math.sin(angle * math.PI / 180) * axis_x / 4;
            transform.position = tmp;
            float camera_size = Camera.main.orthographicSize;
            float ratio = (float)Screen.height / Screen.width;
            float x = camera_size / ratio;
            if (Vector2.Distance(tmp, target.transform.position) > x)
            {
                Destroy(gameObject);
            }
        }
    }
}
