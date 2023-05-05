using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    [Range(10f, 100f)]
    float speed = 20f;

    void Update()
    {
        Vector2 wp = TobiiHelper.getWorldPoint();
        if (wp.x > transform.position.x)
        {
            transform.Translate(speed * Vector3.right * 1 * Time.deltaTime);
        }
        else if (wp.x < transform.position.x)
        {
            transform.Translate(speed * Vector3.right * -1 * Time.deltaTime);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -22f, 13.5f),
        transform.position.y, transform.position.z);
    }
}
 