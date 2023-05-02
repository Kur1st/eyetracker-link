using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

//this is a class that provides easy use for Tobii tracker
public class TobiiHelper : MonoBehaviour
{
    GazePoint lastGazePoint;
    Vector2 getWorldPoint()
    {
        GazePoint gp = TobiiAPI.GetGazePoint();
        if (gp.IsValid)
        {
            lastGazePoint = gp;
            return Camera.main.ScreenToWorldPoint(gp.Screen);
        }
        else return Camera.main.ScreenToWorldPoint(lastGazePoint.Screen);
    }
    Vector2 getViewportPoint()
    {
        GazePoint gp = TobiiAPI.GetGazePoint();
        if (gp.IsValid)
        {
            lastGazePoint = gp;
            return Camera.main.ScreenToViewportPoint(gp.Screen);
        }
        else return Camera.main.ScreenToViewportPoint(lastGazePoint.Screen);
    }

    // Start is called before the first frame update
    void Start()
    {
        lastGazePoint = TobiiAPI.GetGazePoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}