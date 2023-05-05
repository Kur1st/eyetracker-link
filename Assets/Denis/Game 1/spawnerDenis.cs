using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerDenis : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    float RandX;
    Vector2 whereToSpawn;
    [SerializeField]
    public static float spawnRate = 2f;
    float nextSpawn = 0.0f;
    public static Rigidbody2D massa;
    public static float speedUp = 10f;
    public static float restart = 0;

    private void Awake()
    {
        massa = obj.GetComponent<Rigidbody2D>();
        massa.mass = 10;
    }


    void Update()
    {

        transform.position = new Vector2(0, 6 + (Time.time/speedUp)-restart);

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            spawnRate -= (spawnRate/30);
            RandX = Random.Range(-7.37f, 7.57f);
            whereToSpawn = new Vector2(RandX, transform.position.y);
            Instantiate(obj, whereToSpawn, Quaternion.identity);
        }
    }
}
