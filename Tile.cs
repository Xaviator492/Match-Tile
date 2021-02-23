using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{

    public TileType Type; 

    public Vector2 Pos;

    //declare a SpriteRenderer object
    SpriteRenderer rend;
    public bool Activated { get { return Type != TileType.None; } }
    

    private void Awake()
    {

        rend = GetComponent<SpriteRenderer>(); 
    }

    public void Deactivate(float duration)
    {
        Debug.Log("Deactivating " + gameObject.name);
        Type = TileType.None;
        transform.DOScale(1.2f, duration / 2f).OnComplete(() => 
        {
            transform.DOScale(0f, duration / 2f);
            rend.DOFade(0f, duration/2f);
            transform.localScale = Vector3.one;
            Destroy(gameObject);
        });
    }

    public void Activate(int x, int y, TileType type, float duration)
    {
        Pos.x = x;
        Pos.y = y;
        SetType(type);
        rend.DOFade(1f, duration / 2f);
        transform.DOScale(1f, duration / 2f).OnComplete(()=>
        {
            transform.DOScale(0.9f, duration / 2f);
        });
    }

    public void Drop(int steps, float duration)
    {
        transform.DOMove(new Vector3(Pos.x, Pos.y - steps, 0f), duration);       
    }


    public void SetType(TileType tileType)
    {
        
        Type = tileType;

        
        switch (tileType)
        {
            //switch checks what case it is. case = what is the value of switched item (tileType) this time

            case TileType.Blue:
                //if blue, set color to blue
                rend.color = Color.blue;
                break;

            case TileType.Red:
                rend.color = Color.red;
                break;

            case TileType.Green:
                rend.color = Color.green;
                break;

            case TileType.Yellow:
                rend.color = Color.yellow;
                break;

            case TileType.Cyan:
                rend.color = Color.cyan;
                break;
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
    Cyan //=5

}
