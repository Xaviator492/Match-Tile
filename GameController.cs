using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //GameController is used to set the movement of the tile class and user input
    //Can be used to manipulate another PreFab if required

    public static GameController Instance;

    public Board Board;

    private Tile target = null;
    Camera camera;

    public Text ScoreText;

    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreText.text = "Score: " + score.ToString();
        }
    }

    private List<Tile> pool = new List<Tile>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
        camera = FindObjectOfType<Camera>();
    }

    public bool AllowInput = true;

    Vector3 touchDownPos;
    Vector3 touchUpPos;


    private void Update()
    {
        if (AllowInput)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero, 20);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Tile")
                {
                    target = hit.collider.GetComponent<Tile>();
                    touchDownPos = pos;
                    if (!target.Activated)
                    {
                        target = null;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (target != null)
            {
                touchUpPos = camera.ScreenToWorldPoint(Input.mousePosition);
                HandleMovement();
                target = null;
            }
        }

    }

    private void HandleMovement()
    {
        float fltX = touchUpPos.x - touchDownPos.x; //direction traveled on x axis
        float fltY = touchUpPos.y - touchDownPos.y; //direction traveled on y axis
        float intX = Mathf.Abs(fltX); //distance traveled on x axis
        float intY = Mathf.Abs(fltY); //distance traveled on y axis

        if (intX > intY)
        {
            //move horizontally
            if (fltX > 0)
            {
                
                if (Board.Width > target.Pos.x + 1) //if out of bounds, do nothing
                {
                    //move right
                    //Debug.Log("Moved right");
                    Board.SwapTile(target, Vector2.right);
                }
            }
            else
            {
                if (target.Pos.x - 1 >= 0)
                {
                    //move left
                   // Debug.Log("Moved left");
                    Board.SwapTile(target, Vector2.left);
                }
            }

        }
        else
        {
            //move vertically
            if (fltY > 0)
            {
                if (Board.Height > target.Pos.y + 1)
                {
                    //move up
                    //Debug.Log("Moved up");
                    Board.SwapTile(target, Vector2.up);
                }
            }
            else if (fltY < 0)
            {
                if (target.Pos.y - 1 >= 0)
                {
                    //move down
                    //Debug.Log("Moved down");
                    Board.SwapTile(target, Vector2.down);

                }
            }
        }
    }

    public void RemoveTile(int x, int y)
    {
        pool.Add(Board.Tiles[x, y]);
        Board.Tiles[x, y] = null;
    }

    public Tile GetTile()
    {
        Tile tile = null;
        if (pool.Count > 0)
        {
            tile = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        
        return tile;
    }

}
