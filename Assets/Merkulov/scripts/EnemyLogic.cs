using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public GameObject target;
    public float speedStart = 1.4f;
    public float speedBonus = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedBonus += 0.02f * Time.deltaTime;
        float spd = (speedBonus + speedStart) * Time.deltaTime;
        Vector3 tmp = Vector2.MoveTowards(transform.position, target.transform.position, spd);
        tmp.z = 0;
        transform.position = tmp;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyLogic>() == null)
            Destroy(collision.gameObject);
    }

    public void SelfDestruction()
    {
        Destroy(gameObject);
    }
}
