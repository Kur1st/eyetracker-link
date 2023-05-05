using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishScore : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text scoreText;
    public TMP_Text RecordText;
    public static int record=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (star.money > record)
        {
            record = star.money;
        }
        scoreText.text = "Твой счёт: " + star.money.ToString();
        RecordText.text = "Рекорд: " + record.ToString();
    }
}
