
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField]
    new Rigidbody2D rigidbody;

    [SerializeField]
    Sprite HP1Sprite;
    [SerializeField]
    Sprite HP0Sprite;
    [SerializeField]
    public Sprite HP2Sprite;
    public TMP_Text ScoreDisplayTxt;
    public static int Score;

    public static int lifes;
    public TMP_Text LifesDisplayTxt;

    public GameObject Platform;
    public float SpeedStartX = 10;
    public float SpedStartY = 20;
    public float MaxSpeedX = 17;
    public float MaxSpeedY = 18;

    public ParticleSystem PartSyst;
    public ParticleSystem PartSystChein;

    void Start()
    {
        rigidbody.velocity = new Vector2(Random.Range(-SpeedStartX, SpeedStartX), Random.Range(SpedStartY - 5f, SpedStartY + 5f));

        Score = 0;
        lifes = 3;
        LifesDisplayTxt.text = lifes.ToString();
        ScoreDisplayTxt.text = Score.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Loss"))
        {
            lifes--;
            LifesDisplayTxt.text = lifes.ToString();
            transform.position = new Vector3(Platform.transform.position.x, Platform.transform.position.y + 2.2f);
            rigidbody.velocity = new Vector2(Random.Range(-SpeedStartX, SpeedStartX), Random.Range(SpedStartY - 5f, SpedStartY + 5f));
        }

        if (collision.gameObject.CompareTag("BlockHP0"))
        {
            PartSyst.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y);
            PartSyst.Play();
            Destroy(collision.gameObject);

            Score += 100;
            ScoreDisplayTxt.text = Score.ToString();
        }
        if (collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.tag = "BlockHP0";
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = HP0Sprite;
        }
        if (collision.gameObject.CompareTag("BlockHP2"))
        {

            collision.gameObject.tag = "Block";
            PartSystChein.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y);
            PartSystChein.Play();
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = HP1Sprite;
        }
        void Update()
        {
            Debug.Log("" + rigidbody.velocity);
            if (rigidbody.velocity.x > MaxSpeedX)
            {
                rigidbody.velocity = new Vector2(MaxSpeedX, rigidbody.velocity.y);
            }
            else if (rigidbody.velocity.y > MaxSpeedY)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.y, MaxSpeedY);
            }
        }


    }
}
