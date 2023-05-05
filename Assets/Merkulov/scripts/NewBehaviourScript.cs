using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NewBehaviourScript : MonoBehaviour
{
    public short collectedFood = 0;
    public GameObject Armor;
    public GameObject Walking;
    public GameObject Text;
    public long score = 0;
    public float bst = 2;
    public float speed = 3.5f;
    private GameObject particleThing;
    private float invulnarability=0;
    private List<GameObject> ArmorPieces = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-10, 0, 0);
        particleThing = Object.Instantiate(Walking, transform);
    }

    // Update is called once per frame
    void Update()
    {
        invulnarability -= 1 * Time.deltaTime;
        Vector3 place = get_position.GetPosition();
        transform.right = (Vector2)(place - transform.position).normalized;
        /*
        float angle = CalculateAngle(transform.position, place);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        */
        Vector3 tmp = Vector3.MoveTowards(transform.position, place, speed * Time.deltaTime * bst);
        tmp.z = 0;
        transform.position = tmp;
        particleThing.GetComponent<ParticleSystem>().startSpeed = tmp.x * Time.deltaTime;
        bst -= 0.39f * Time.deltaTime;
        if (bst < 1) { 
            bst = 1;
        }
        if (invulnarability < 0)
        {
            invulnarability = 0;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FoodLogic>() != null)
        { 
            Destroy(collision.gameObject);
            score += 100;
            collectedFood++;
            if (collectedFood % 3 == 0) {
                if (ArmorPieces.Count == 0)
                    ArmorPieces.Add(Creater.CreateArmorObject(Armor, gameObject));
                else

                    ArmorPieces.Add(Creater.CreateArmorObject(Armor, gameObject, ArmorPieces[ArmorPieces.Count - 1].GetComponent<ArmorLogic>().angle + 72));
            }
            CreateText();
            if (collectedFood >= 15)
                collectedFood = 16;
        }
        if (collision.gameObject.GetComponent<EnemyLogic>() != null)
        {
            if (collectedFood >= 15) { 
                score += 1000;
                if (invulnarability != 0)
                    CreateText();
                collision.gameObject.GetComponent<EnemyLogic>().SelfDestruction();
            }
            if (invulnarability == 0)
            {
                if (collectedFood <= 8) { 
                    Destroy(gameObject);
                }
                score += 25;
                collectedFood = 0;
                invulnarability = 2.5f;
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                bst = 5;
                for (int i = 0; i < ArmorPieces.Count; ++i)
                {
                    ArmorPieces[i].GetComponent<ArmorLogic>().is_good = false;
                }
                ArmorPieces.Clear();
                CreateText();
            }
        }
    }

    public float CalculateAngle(Vector3 first, Vector3 second)
    {
        return Vector2.Angle(Vector2.right, second - first);
        //return Mathf.Atan(y / x) * 180 / Mathf.PI;
    }

    private void CreateText()
    {
        GameObject obj = Instantiate(Text);
        obj.transform.position = transform.position;
        obj.GetComponent<TextLogic>().value = score.ToString() + " " + ArmorPieces.Count.ToString() + "/5";
    }
}
