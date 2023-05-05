using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarScore : MonoBehaviour
{
    public float score;
    public TMP_Text textScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        score = (int)(Car.rightDrowing * 100f);
        Debug.Log(Car.rightDrowing);
        textScore.text = "Результат: " + score.ToString() + "%";
    }
}
