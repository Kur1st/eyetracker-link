using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class star : MonoBehaviour
{
    public static int money;
    public static int life = 3;

    public void OpenMenu()
    {
        SceneManager.LoadScene("GameOverMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("destr"))
        {
            Destroy(gameObject);
            life--;
            if(life == 0)
            {
                OpenMenu();
            }
        }
        if (collision.CompareTag("player"))
        {
            money++;
            Destroy(gameObject);

            if (money % 10 == 0)
            {
                life++;
            }

        }
    }
}
