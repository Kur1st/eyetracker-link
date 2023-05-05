using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour, Movable
{
    int x, y;
    [SerializeField] UnityEvent left, top, right, bottom;
    [SerializeField] float focusTime;
    [SerializeField] GameObject tobiiCursor;
    private float timeOnFocus = 0;
    private bool leftAnimating = false;
    private bool topAnimating = false;
    private bool rightAnimating = false;
    private bool bottomAnimating = false;
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
        SpriteRenderer sr = GameInstanceBehaviour.field[x, y].GetComponent<SpriteRenderer>();
        Debug.Log("player");
        Vector3 bot_left = transform.TransformPoint(sr.sprite.bounds.min);
        Debug.Log(bot_left);
        Vector3 top_right = transform.TransformPoint(sr.sprite.bounds.max);
        Debug.Log(top_right);
        Vector3 wp = TobiiCursorBehaviour.world_point;
        Debug.Log(wp);
        checkLeft();
        checkTop();
        checkRight();
        checkBottom();
        Debug.Log("Animating: " + leftAnimating + " " + topAnimating + " " + rightAnimating + " " + bottomAnimating);
    }

    void checkLeft()
    {
        if (x - 1 >= GameInstanceBehaviour.width || x - 1 < 0)
            return;
        SpriteRenderer sr = GameInstanceBehaviour.field[x-1, y].GetComponent<SpriteRenderer>(); 
        Debug.Log("left");
        Vector3 bot_left = transform.TransformPoint(sr.sprite.bounds.min) + (GameInstanceBehaviour.field[x - 1, y].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(bot_left);
        Vector3 top_right = transform.TransformPoint(sr.sprite.bounds.max) + (GameInstanceBehaviour.field[x - 1, y].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(top_right);
        Vector3 wp = TobiiCursorBehaviour.world_point;
        Debug.Log(wp);
        //if you want you can use direct sight on the button but it will shake and may cause a misclick
        //against it using cursor will improve stability
        //Vector3 wp = TobiiHelper.getWorldPoint();
        if (wp.y >= bot_left.y && wp.y <= top_right.y && wp.x >= bot_left.x && wp.x <= top_right.x)
        {
            if (timeOnFocus == 0)
            {
                leftAnimating = true;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().animateCursor(focusTime);
            }

            timeOnFocus += Time.deltaTime;
        }
        else
        {
            if (leftAnimating)
            {
                timeOnFocus = 0;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
                leftAnimating = false;
            }
        }

        if (timeOnFocus >= focusTime && leftAnimating)
        {
            leftAnimating = false;
            tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
            Debug.Log("Go left");
            left.Invoke();
            timeOnFocus = 0;
        }
    }
    void checkTop()
    {
        if (y + 1 >= GameInstanceBehaviour.height || y + 1 < 0)
            return;
        SpriteRenderer sr = GameInstanceBehaviour.field[x, y+1].GetComponent<SpriteRenderer>();
        Debug.Log("top");
        Vector3 bot_left = transform.TransformPoint(sr.sprite.bounds.min) + (GameInstanceBehaviour.field[x, y + 1].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(bot_left);
        Vector3 top_right = transform.TransformPoint(sr.sprite.bounds.max) + (GameInstanceBehaviour.field[x, y + 1].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(top_right);
        Vector3 wp = TobiiCursorBehaviour.world_point;
        Debug.Log(wp);
        //if you want you can use direct sight on the button but it will shake and may cause a misclick
        //against it using cursor will improve stability
        //Vector3 wp = TobiiHelper.getWorldPoint();
        if (wp.y >= bot_left.y && wp.y <= top_right.y && wp.x >= bot_left.x && wp.x <= top_right.x)
        {
            if (timeOnFocus == 0)
            {
                topAnimating = true;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().animateCursor(focusTime);
            }

            timeOnFocus += Time.deltaTime;
        }
        else
        {
            if (topAnimating)
            {
                timeOnFocus = 0;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
                topAnimating = false;
            }
        }

        if (timeOnFocus >= focusTime && topAnimating)
        {
            topAnimating = false;
            tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
            top.Invoke();
            timeOnFocus = 0;
        }
    }
    void checkRight()
    {
        if (x + 1 >= GameInstanceBehaviour.width || x + 1 < 0)
            return;
        SpriteRenderer sr = GameInstanceBehaviour.field[x+1, y].GetComponent<SpriteRenderer>();
        Debug.Log("right");
        Vector3 bot_left = transform.TransformPoint(sr.sprite.bounds.min) + (GameInstanceBehaviour.field[x + 1, y].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(bot_left);
        Vector3 top_right = transform.TransformPoint(sr.sprite.bounds.max) + (GameInstanceBehaviour.field[x + 1, y].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(top_right);
        Vector3 wp = TobiiCursorBehaviour.world_point;
        Debug.Log(wp);
        //if you want you can use direct sight on the button but it will shake and may cause a misclick
        //against it using cursor will improve stability
        //Vector3 wp = TobiiHelper.getWorldPoint();
        if (wp.y >= bot_left.y && wp.y <= top_right.y && wp.x >= bot_left.x && wp.x <= top_right.x)
        {
            if (timeOnFocus == 0)
            {
                rightAnimating = true;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().animateCursor(focusTime);
            }

            timeOnFocus += Time.deltaTime;
        }
        else
        {
            if (rightAnimating)
            {
                timeOnFocus = 0;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
                rightAnimating = false;
            }
        }

        if (timeOnFocus >= focusTime && rightAnimating)
        {
            rightAnimating = false;
            tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
            Debug.Log("Go right");
            right.Invoke();
            timeOnFocus = 0;
        }
    }
    void checkBottom()
    {
        if (y - 1 >= GameInstanceBehaviour.height || y - 1 < 0)
            return;
        SpriteRenderer sr = GameInstanceBehaviour.field[x, y-1].GetComponent<SpriteRenderer>();
        Debug.Log("bottom");
        Vector3 bot_left = transform.TransformPoint(sr.sprite.bounds.min) + (GameInstanceBehaviour.field[x, y - 1].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(bot_left);
        Vector3 top_right = transform.TransformPoint(sr.sprite.bounds.max) + (GameInstanceBehaviour.field[x, y - 1].transform.position - GameInstanceBehaviour.field[x, y].transform.position);
        Debug.Log(top_right);
        Vector3 wp = TobiiCursorBehaviour.world_point;
        Debug.Log(wp);
        //if you want you can use direct sight on the button but it will shake and may cause a misclick
        //against it using cursor will improve stability
        //Vector3 wp = TobiiHelper.getWorldPoint();
        if (wp.y >= bot_left.y && wp.y <= top_right.y && wp.x >= bot_left.x && wp.x <= top_right.x)
        {
            if (timeOnFocus == 0)
            {
                bottomAnimating = true;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().animateCursor(focusTime);
            }

            timeOnFocus += Time.deltaTime;
        }
        else
        {
            if (bottomAnimating)
            {
                timeOnFocus = 0;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
                bottomAnimating = false;
            }
        }

        if (timeOnFocus >= focusTime && bottomAnimating)
        {
            bottomAnimating = false;
            tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
            Debug.Log("Go bottom");
            bottom.Invoke();
            timeOnFocus = 0;
        }
    }
}
