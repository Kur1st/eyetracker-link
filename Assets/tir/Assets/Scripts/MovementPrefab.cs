using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementPrefab : MonoBehaviour
{
    public Transform SpawnPos;
    public Vector2 direction;
    public GameObject ballon;
    public GameObject someself;
    public Sprite newSprite;
    public Sprite[] balloons;
    public GameObject[] spawners;
    private int rand;
    private int rand1;

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
        Destroy(ballon, 0.5f);
    }
    void Start()
    {
        rand = Random.Range(0, balloons.Length);
        rand1 = Random.Range(0, balloons.Length);

        GetComponent<SpriteRenderer>().sprite = balloons[rand];
        StartCoroutine(SpawnCD());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction);
    }
    void Repeat()
    {
        StartCoroutine(SpawnCD());
    }
    IEnumerator SpawnCD()
    {
        yield return new WaitForSeconds(4);
        Instantiate(ballon, SpawnPos.position, Quaternion.identity);
        Repeat();
    }
}
