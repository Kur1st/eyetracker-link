using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_position
{
    public static Vector3 GetPosition()
    {
        Vector3 position = TobiiHelper.getWorldPoint();
        return position;
    }
}
