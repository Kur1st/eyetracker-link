using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

//this is a class that provides easy use for Tobii tracker
public class TobiiHelper : MonoBehaviour
{
    public static GazePoint lastGazePoint;
    public static Vector2 getWorldPoint()
    {
        if (TobiiAPI.IsConnected)
        {
            GazePoint gp = TobiiAPI.GetGazePoint();
            if (gp.IsValid)
            {
                lastGazePoint = gp;
                return Camera.main.ScreenToWorldPoint(gp.Screen);
            }
            else return Camera.main.ScreenToWorldPoint(lastGazePoint.Screen);
        }
        else
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static Vector2 getViewportPoint()

    {
        if (TobiiAPI.IsConnected)
        {
            GazePoint gp = TobiiAPI.GetGazePoint();
            if (gp.IsValid)
            {
                lastGazePoint = gp;
                return Camera.main.ScreenToViewportPoint(gp.Screen);
            }
            else return Camera.main.ScreenToViewportPoint(lastGazePoint.Screen);
        }
        else
            return Camera.main.ScreenToViewportPoint(Input.mousePosition);
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