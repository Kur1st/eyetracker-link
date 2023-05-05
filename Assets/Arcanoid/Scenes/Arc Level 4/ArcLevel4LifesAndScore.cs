using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcLevel4LifesAndScore : MonoBehaviour
{
    // Update is called once per frame
    private void Start()
    {
        Debug.Log("AA");
    }
    void Update()
    {
        if (BallController.lifes == 0)
        {
            BallController.lifes = 3;
            SceneManager.LoadScene("Arc LVL4 GameOver");
        }
        if (BallController.Score == 2400)
        {
            //BallController.Score = 0;
            SceneManager.LoadScene("Arc Level4 Win");
        }
    }   
}
