using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationBehaviour : MonoBehaviour
{
    public static Vector2 calibration = Vector2.zero;
    [SerializeField] public int calibrationCounter;
    [SerializeField] public GameObject sceneChange;
    // Start is called before the first frame update
    void Start()
    {
        TobiiHelper.calibration = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (calibrationCounter <= 0)
        {
            TobiiHelper.calibration = calibration;
            sceneChange.GetComponent<Scene>().ChangeScene("MainMenu");
        }
    }

    public void addCalibration(Vector2 range)
    { 
        calibration += range;
        calibrationCounter -= 1;
    }
}
