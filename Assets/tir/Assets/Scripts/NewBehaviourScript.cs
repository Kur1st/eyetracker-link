using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject obj;
    public Sprite[] baloons;
    public Sprite babah;
    public float startingSpawnRate = 5;
    public float currentSpawnRate = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawnRate = startingSpawnRate / 2;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnRate -= Time.deltaTime;
        if (currentSpawnRate <= 0)
        {
            currentSpawnRate = startingSpawnRate;
            int rand = Random.Range(0, baloons.Length);
            GameObject baloon = Instantiate(obj);
            baloon.AddComponent<Movement>();
            baloon.AddComponent<BoxCollider2D>();
            baloon.GetComponent<SpriteRenderer>().sprite = baloons[rand];
            baloon.GetComponent<Movement>().direction = new Vector2(-Random.Range(0.025f, 0.1f), Random.Range(-0.01f, 0.01f));
            baloon.GetComponent<Movement>().newSprite = babah;
            Vector3 tmp = transform.position;
            tmp.y = Random.Range(-1.5f, 1.5f);
            baloon.transform.position = tmp;
        }
    }
}
