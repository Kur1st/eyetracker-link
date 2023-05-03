using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, Movable
{
    int x, y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool move(int dx, int dy)
    {
        if (x + dx >= GameInstanceBehaviour.width || x + dx < 0 || y + dy >= GameInstanceBehaviour.height || y + dy < 0)
        {
            Debug.Log("borders");
            return false;
        }
        if (GameInstanceBehaviour.field[x + dx, y + dy].CompareTag("Wall"))
        {
            Debug.Log("walls");
            return false;
        }
        else if (GameInstanceBehaviour.field[x + dx, y + dy].CompareTag("Empty") || GameInstanceBehaviour.field[x + dx, y + dy].CompareTag("Goal"))
        {
            if (GameInstanceBehaviour.movables[x + dx, y + dy] != null)
            {
                if (GameInstanceBehaviour.movables[x + dx, y + dy].CompareTag("Box"))
                {
                    if (GameInstanceBehaviour.movables[x + dx, y + dy].GetComponent<Movable>().move(dx, dy))
                    {
                        Debug.Log("box movable");
                        transform.position += new Vector3(dx, dy) * GameInstanceBehaviour.scale;
                        GameObject swap = GameInstanceBehaviour.movables[x+dx, y+dy];
                        GameInstanceBehaviour.movables[x + dx, y + dy] = GameInstanceBehaviour.movables[x, y];
                        GameInstanceBehaviour.movables[x, y] = swap;
                        x += dx;
                        y += dy;
                        Debug.Log(x + " " + y);
                        return true;
                    }
                    else
                    {
                        Debug.Log("box immovable");
                        return false;
                    }
                }
            }
            else
            {
                Debug.Log("empty");
                transform.position += new Vector3(dx, dy) * GameInstanceBehaviour.scale;
                GameObject swap = GameInstanceBehaviour.movables[x + dx, y + dy];
                GameInstanceBehaviour.movables[x + dx, y + dy] = GameInstanceBehaviour.movables[x, y];
                GameInstanceBehaviour.movables[x, y] = swap;
                x += dx;
                y += dy;
                Debug.Log(x + " " + y);
                return true;
            }
        }
        return false;
    }

    public void setPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
