using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameInstanceBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    bool left, right, up, down;
    public static int height;
    public static int width;
    public static float scale;
    public static GameObject[,] field;
    public static GameObject[,] movables;
    public static GameObject[,] goals;
    private GameObject player;

    private TextAsset[] levels;
    int lvl_num;

    [SerializeField] public GameObject empty;
    [SerializeField] public GameObject wall;
    [SerializeField] public GameObject box;
    [SerializeField] public GameObject goal;
    [SerializeField] public GameObject mover;
    [SerializeField] public GameObject goalPoint;

    void Start()
    {
        lvl_num = 0;
        levels = Resources.LoadAll<TextAsset>("Sokoban");
        Debug.Log(levels.Length + " levels loaded");
        loadLevel();
    }

    private void parseLevel(TextAsset file)
    {
        //width height
        //in matrix 0 is empty space, 1 is wall, 2 is box, 3 is goal, 4 is player
        string[] lines = file.text.Split('\n');
        string[] s = lines[0].Split(' ');
        height = int.Parse(s[1]);
        width = int.Parse(s[0]);
        field = new GameObject[width, height];
        movables = new GameObject[width, height];
        goals = new GameObject[width, height];

        Vector2 bot_left = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 top_right = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 middle = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Vector2 startPos;
        if ((top_right.y - bot_left.y) / height <=  (top_right.x - bot_left.x) / width)
        {
            scale = (top_right.y - bot_left.y) / height;
            startPos = new Vector2(middle.x - scale * width / 2, bot_left.y);
        }
        else
        {
            scale = (top_right.x - bot_left.x) / width;
            startPos = new Vector2(bot_left.x, middle.y - scale * height / 2);
        }

        for (int i = 0; i< width; i++)
        {
            s = lines[i + 1].Split();
            int y = height - i - 1;
            for (int x = 0; x < width; x++)
            {
                if (s[x] == "0") //empty
                {
                    field[x, y] = Instantiate(empty);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale , 1);
                }
                else if (s[x] == "1") //wall
                {
                    field[x, y] = Instantiate(wall);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                }
                else if (s[x] == "2") //box
                {
                    field[x, y] = Instantiate(empty);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                    movables[x, y] = Instantiate(box);
                    movables[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    movables[x, y].GetComponent<Movable>().setPos(x, y);
                    toScale = 1 / movables[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    movables[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                }
                else if (s[x] == "3") //goal
                {
                    field[x, y] = Instantiate(goal);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                    goals[x, y] = Instantiate(goalPoint);
                    goals[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    toScale = 1 / goals[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    goals[x, y].transform.localScale = new Vector3(toScale * scale * 0.5f, toScale * scale * 0.5f, 1);
                }
                else if (s[x] == "4") //player
                {
                    field[x, y] = Instantiate(empty);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale  * scale, toScale * scale, 1);
                    player = Instantiate(mover);
                    movables[x, y] = player;
                    movables[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    player.GetComponent<Movable>().setPos(x, y);
                    toScale = 1 / movables[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    movables[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                }
            }
        }
    }

    void loadLevel()
    {
        Debug.Log("Level: " + (lvl_num+1));
        if (lvl_num != 0)
            destroyAll();
        if (lvl_num < levels.Length)
        {
            parseLevel(levels[lvl_num]);
        }
        else
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    void destroyAll()
    {
        foreach (GameObject i in movables)
        {
            if (i != null)
                Destroy(i);
        }
        foreach(GameObject i in field)
        {
            if (i != null)
                Destroy(i);
        }
        foreach (GameObject i in goals)
        {
            if (i != null)
                Destroy(i);
        }
    }

    public void restart()
    {
        destroyAll();
        loadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") || left)
        {
            player.GetComponent<Movable>().move(-1, 0);
            left = false;
            Debug.Log("event left");
        }
        else if (Input.GetKeyDown("w") || up)
        {
            player.GetComponent<Movable>().move(0, 1);
            up = false;
            Debug.Log("event up");
        }
        else if (Input.GetKeyDown("d") || right)
        {
            player.GetComponent<Movable>().move(1, 0);
            right = false;
            Debug.Log("event right");
        }
        else if (Input.GetKeyDown("s") || down)
        {
            player.GetComponent<Movable>().move(0, -1);
            down = false;
            Debug.Log("event down");
        }

        bool flag = true;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!(field[x,y] == null) && field[x, y].CompareTag("Goal"))
                {
                    if (movables[x, y] == null || !movables[x, y].CompareTag("Box"))
                        flag = false;
                }
            }
        }
        if (flag)
        {
            destroyAll();
            lvl_num++;
            loadLevel();
        }
    }

    public void goLeft()
    {
        left = true;
    }
    public void goUp()
    {
        up = true;
    }
    public void goRight()
    {
        right = true;
    }
    public void goDown()
    {
        down = true;
    }
}
