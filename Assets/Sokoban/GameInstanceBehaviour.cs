using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameInstanceBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static int height;
    public static int width;
    public static float scale;
    public static GameObject[,] field;
    public static GameObject[,] movables;
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
        nextLevel();
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

        Debug.Log(scale);

        for (int i = 0; i< width; i++)
        {
            s = lines[i + 1].Split();
            int y = height - i - 1;
            for (int x = 0; x < width; x++)
            {
                if (s[x] == "0")
                {
                    field[x, y] = Instantiate(empty);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale , 1);

                }
                else if (s[x] == "1")
                {
                    field[x, y] = Instantiate(wall);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                }
                else if (s[x] == "2")
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
                else if (s[x] == "3")
                {
                    field[x, y] = Instantiate(goal);
                    field[x, y].transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    float toScale = 1 / field[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    field[x, y].transform.localScale = new Vector3(toScale * scale, toScale * scale, 1);
                    GameObject gp = Instantiate(goalPoint);
                    gp.transform.position = startPos + new Vector2(x + 0.5f, y + 0.5f) * scale;
                    toScale = 1 / gp.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
                    gp.transform.localScale = new Vector3(toScale * scale * 0.5f, toScale * scale * 0.5f, 1);
                }
                else if (s[x] == "4")
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

    void nextLevel()
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
        lvl_num++;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
            player.GetComponent<Movable>().move(-1, 0);
        else if (Input.GetKeyDown("w"))
            player.GetComponent<Movable>().move(0, 1);
        else if (Input.GetKeyDown("d"))
            player.GetComponent<Movable>().move(1, 0);
        else if (Input.GetKeyDown("s"))
            player.GetComponent<Movable>().move(0, -1);

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
            nextLevel();
        }
    }
}
