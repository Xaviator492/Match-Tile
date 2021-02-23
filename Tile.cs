using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public TileType Type; //variable to refer to the enum var
    //declare a SpriteRenderer object
    SpriteRenderer rend;

    private Vector2 firstTouchPosition; //when the mouse is first clicked or screen first touched
    private Vector2 finalTouchPosition; //when lmb is released or finger lifted off screen
    public float SwipeAngle = 0;
    public int X;
    public int Y;
    Board board; //reference to the board



    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        board = FindObjectOfType<Board>();
    }
    private void Update()
    {
        X = (int)transform.position.x;
        Y = (int)transform.position.y;
        //store the position of this tile
    }

    #region Set Up
    public void SetType(TileType tileType)
    {
        Type = tileType;

        switch (tileType)
        {
            //switch checks what case it is. case = what is the value of switched item (tileType)
            //use the sprite renderer to render the tiles in different colours
            case TileType.Red:
                rend.color = Color.red;
                break;

            case TileType.Blue:
                rend.color = Color.blue;
                break;

            case TileType.Yellow:
                rend.color = Color.yellow;
                break;

            case TileType.Green:
                rend.color = Color.green;
                break;

            case TileType.Cyan:
                rend.color = Color.cyan;
                break;
        }

    }


    #endregion Set Up

    private void OnMouseDown()
    {
        //record where the first press or click is
        firstTouchPosition = Input.mousePosition;
        Debug.Log(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        //record where the release is
        finalTouchPosition = Input.mousePosition;
        Movement();
    }

    private void Movement()
    {
        float fltX = finalTouchPosition.x - firstTouchPosition.x; //direction traveled on x axis
        float fltY = finalTouchPosition.y - firstTouchPosition.y; //direction traveled on y axis
        float intX = Mathf.Abs(fltX); //distance traveled on x axis
        float intY = Mathf.Abs(fltY); //distance traveled on y axis

        if(intX > intY)
        {
            //move horizontally
            if(fltX > 0)
            {
                //move right
                transform.position = new Vector3(X + 1, Y, 10f);
                board.SwapTile(X, Y, 0); //ask the Board class to swap tiles and update the array
            } else
            {
                //move left
                transform.position = new Vector3(X - 1, Y, 10f);
                board.SwapTile(X, Y, 1); //ask the Board class to swap tiles and update the array
            }

        } else
        {
            //move vertically
            if(fltY > 0)
            {
                //move up
                transform.position = new Vector3(X, Y + 1, 10f);
                board.SwapTile(X, Y, 2); //ask the Board class to swap tiles and update the array
            } else
            {
                //move down
                transform.position = new Vector3(X, Y - 1, 10f);
                board.SwapTile(X, Y, 3); //ask the Board class to swap tiles and update the array
            }
        }


    }


}

public enum TileType
{
    None, //default = 0
    Red, //=1
    Blue, //=2
    Yellow, //=3
    Green, //=4
    Cyan, //=5


}
