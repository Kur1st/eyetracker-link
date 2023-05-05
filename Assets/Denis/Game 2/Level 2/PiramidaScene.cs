using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PiramidaScene : MonoBehaviour
{
    public float maximumy = 0.5853f;
    public float maximumx = 0.7159f;
    public GameObject car;
    public GameObject TestLine;
    private bool flag=true;
    public float timeScene=10000f;
    // Start is called before the first frame update
    void Start()
    {
        timeScene = 1000f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Car.rightDrowing > 0.7f && flag)
        {
            StartCoroutine(SlowScale());
            flag = false;
            TestLine.SetActive(false);
            timeScene = Time.time;
            Debug.Log(timeScene);
        }
        if (Time.time > timeScene + 1.5f)
        SceneManager.LoadScene("PiramidaWin");

        if (Car.rightDrowing < 0.7f && Car.rightDrowing!=0f && flag)
        {
            SceneManager.LoadScene("PiramidaGameOver");
        }
    }
    IEnumerator SlowScale()
    {
        for (float q = 10; q >0; q --)
        {
            car.transform.localScale = new Vector3(maximumx / q, maximumy / q, q);
            yield return new WaitForSeconds(.05f);
        }
    }
}
