using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Car : MonoBehaviour
{
    public LineRenderer Line;
    public LineRenderer PredLine;

    public float lineWidth = 0.04f;
    public float minimumVertexDistance = 0.1f;
    private float minDist;
    private bool isLineStarted;
    public bool flag;
    public int count;

    public bool end = true;

    public static float rightDrowing;

    Vector3[] PredCoor = new Vector3[1000];
    public void Awake()
    {
        // set the color of the line
        Line.startColor = Color.red;
        Line.endColor = Color.red;
        count = 0;
        // set width of the renderer
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;

        isLineStarted = false;
        Line.positionCount = 0;

        //Vector3[] PredCoor = new Vector3[PredLine.positionCount];
        PredLine.GetPositions(PredCoor);
    }
    public void buttonClick1()
    {
        if (flag)
        {
            flag = false;
            end = true;
        }
        else
            flag = true;

    }
    void Update()
    {
        if (flag && !isLineStarted) // запускать по нажатию на кнопку
        {
            Line.positionCount = 0;
            Vector2 mousePos = GetWorldCoordinate(Input.mousePosition);

            Line.positionCount = 2;
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            isLineStarted = true;
        }

        if (isLineStarted)
        {
            Vector3 currentPos = GetWorldCoordinate(Input.mousePosition);
            Debug.Log(currentPos);
            float distance = Vector2.Distance(currentPos, Line.GetPosition(Line.positionCount - 1));
            if (distance > minimumVertexDistance)
            {
                //Debug.Log(distance);
                UpdateLine();
                Vector3 coor = Line.GetPosition(Line.positionCount - 1);
                Vector2 pt = new Vector2(currentPos[0], currentPos[1]);
                double minDist = 1000000;
                for (int i = 0; i < PredLine.positionCount - 1; i++)
                {
                    Vector2 p1 = new Vector2(PredCoor[i][0], PredCoor[i][1]);
                    Vector2 p2 = new Vector2(PredCoor[i + 1][0], PredCoor[i + 1][1]);
                    if (FindDistanceToSegment(pt, p1, p2) < minDist)
                    {
                        minDist = FindDistanceToSegment(pt, p1, p2);

                    }

                }
                if (minDist < 0.31f)
                {
                    count++;
                }
                else if (minDist > 0.7f)
                    count--;
            }

        }

        if (!flag && end) // заканчивать при нажатии на кнопку
        {
            rightDrowing = ((float) count / Line.positionCount);
            isLineStarted = false;
            Line.positionCount = 0;
            end = false;
        }
    }

    private void UpdateLine()
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
    }
    private Vector2 GetWorldCoordinate(Vector2 mousePosition)
    {
        Vector3 mousePos = new Vector3(mousePosition.x, mousePosition.y, -9);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Рассчитайте расстояние между
    // точка pt и отрезок p1 -> p2.
    private double FindDistanceToSegment(Vector2 pt, Vector2 p1, Vector2 p2)
    {
        double ax = p1[0];
        double ay = p1[1];
        double bx = p2[0];
        double by = p2[1];
        double x = pt[0];
        double y = pt[1];

        double distanceToA = Math.Sqrt((ax - x) * (ax - x) + (ay - y) * (ay - y));
        double distanceToB = Math.Sqrt((bx - x) * (bx - x) + (by - y) * (by - y));

        if (((x > ax || x > bx) && (x < bx || x < ax)) || ((y > ay || y > by) && (y < by || y < ay)))
        {
            return Math.Abs((by - ay) * x - (bx - ax) * y + bx * ay - by * ax) /
            Math.Sqrt((by - ay) * (by - ay) + (bx - ax) * (bx - ax));
        }
        else
        {
            return Math.Min(distanceToA, distanceToB);
        }
    }
}