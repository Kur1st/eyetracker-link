using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public GameObject InspectedObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = InspectedObject.transform.position;
        transform.position += (vector3 - transform.position) / 1.5f * Time.deltaTime;
        Vector3 tmp = transform.position;
        tmp.z = -10;
        transform.position = tmp;
    }
}
