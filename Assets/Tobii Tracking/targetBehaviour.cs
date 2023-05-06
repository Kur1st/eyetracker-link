using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehaviour : MonoBehaviour
{
    [SerializeField] float calibrationLength;
    [SerializeField] float timeToPoint;
    [SerializeField] GameObject calibrationInstance;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 wp = TobiiHelper.getWorldPoint();
        if ((transform.position - wp).magnitude < calibrationLength)
        {
            time += Time.deltaTime;
            Debug.Log("Calibrating");
            if (time >= timeToPoint)
            {
                calibrationInstance.GetComponent<CalibrationBehaviour>().addCalibration(transform.position - wp);
                Destroy(gameObject);
                Debug.Log("Calibrated");
            }
        }
        else
            time = 0;
    }
}
