using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text Scoredisp;
    // Start is called before the first frame update
    void Start()
    {
        Scoredisp.text = BallController.Score.ToString();
    }
}
