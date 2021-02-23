using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public TileType Type; //we created an enum, and created a Variable of type of our enum 'TitleType'. Default value is first value in enum

    //lets create this method that sets the type to the parsed type, do it. what is the parameters/arguments of the method 

    //lets modify this script so it sets the color when type is set, we need access to SpriteRenderer component before we can do that.

    //declare a SpriteRenderer object
    SpriteRenderer rend;

    //cuz wewerking with 2D sprite. on 3D objects we have MeshRenderer on 2D we have SpriteRenderer. get it?

    private void Awake()
    {

        rend = GetComponent<SpriteRenderer>(); //GetComponent() tries to fetch a component attached to the object this script is attached to.
        //in the <> brackets you specify what type of Component you are looking for. Nao reeds?ye y sprite rendrer?
    }


    public void SetType(TileType tileType)
    {
        //good, try to use informative variable names tho
        Type = tileType;

        //nao lets set the colour. we have to see wat type is set tho. lets use switch statement
        switch (tileType)
        {
            //switch checks what case it is. case = what is the value of switched item (tileType) this time

            case TileType.Blue:
                //if blue, set color 2 blu
                rend.color = Color.blue;
                //break is to mek it brek out of switch. Naht a loop. case? ye it ends switch statement there at break;
                //cuz we don't need 2 do anything else nao after we set the color
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
        }

    }

    //ok? y u no see? i  r plez 48 solitair u ahs hatton yee. u understands till nao?yeeeo k. lez go bak 2 Board script
}

public enum TileType
{
    None, //default = 0
    Red, //=1
    Blue, //=2
    Yellow, //=3
    Green //=4

    //get it? yes
}


/*
 * Ok, about Enums. Enums are basically int values assigned a keyword. Useful when u have a limited set of values to be assigned
 * Like Weapon types, ammo type etc.
 * if u don't specify the int value corresponding to each Enum item, it starts with 0
 */