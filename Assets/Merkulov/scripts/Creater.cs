using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Creater
{
    public static GameObject CreateFoodObject(GameObject obj, Vector3 pos)
    {   
        GameObject food = Object.Instantiate(obj);
        food.transform.position = pos;
        return food;
    }

    public static GameObject CreateArmorObject(GameObject obj, GameObject target, float angle=0)
    {
        GameObject piece = Object.Instantiate(obj);
        piece.GetComponent<ArmorLogic>().target = target;
        piece.GetComponent<ArmorLogic>().angle = angle;
        return piece;
    }
    public static GameObject CreateEnemyObject(GameObject obj, GameObject target)
    {
        GameObject enemy = Object.Instantiate(obj);
        enemy.GetComponent<EnemyLogic>().target = target;
        return enemy;
    }
}
