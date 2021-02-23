using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    //lets use this script to setup our grid.

    public GameObject TilePrefab;  //we declared this to mek it appear on inspector so we can set  a reference to it l8r

    //nao lez create a grid
    //below is an array of type GameObject
    public GameObject[,] Tiles; //the comma separates two possible valuees daz empty rite nao
    //if 3d array, use 2 commas [,,]


    //gameobject is the class that every object in the scene is an instance of. yee?yee

    private void Start()
    {

        Tiles = new GameObject[8, 8]; //this is only one dimensional array. letx mek it 2d array

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                #region Comments
                //u know y i created nested loop? 2 run while thing is going? wat? change y and x yee
                //so we create one tile at x=0 y=0, then move up x=0 y=1...x=0, y=8. then x=0 y=0...x=8 y = 8yee. 64 tilesyeet

                //this statement defines an object of type "GameObject", and assigns it to the value on right of the assignment operator "="
                //the method on the right of the "=" creates instance of an object. In this case, we pass "TilePrefab" reference there.
                //second parameter here is for parent. It expects an object of type Transform. And we know "transform" is Transform attached to this object
                //so we make createed tile a child of the Board object
                #endregion
                GameObject tile = Instantiate(TilePrefab, transform);

                //this sets the reference of "tile" to the x, y index in the Tiles array
                Tiles[x, y] = tile;

                tile.name = string.Format("Tile ({0}, {1})", x, y);
                tile.transform.position = new Vector3(x, y, 0f); //nao we spaced em out based on the Y index the loop is in
            }


        }//loop(s) end here

        //k it's still a bit off. daz cuz. sprites anchor is in middle of it. so x = 4 is the middle point of 5th tile, we need it between 4th and 5th tile
        Camera.main.transform.position = new Vector3(3.5f, 3.5f, -10f);
        //so we created one tile, lets create multiple tiles. do u know hao? no. I will write code to speed up, and explain. u wil understand as I type too
        //1 problem(s) found. We have mani tiles but we dont save their reference so we can use it l8r. lets do that

        RandomizeColours();
        
    }


    //nao lets create a method to initialize the grid with colours. u here?ye
    public void RandomizeColours()
    {
        //write a loop to iterate thru values in Tiles array

        //im on call. write in c#, naht vb. how do I call objects from arrays in C#? w8 on call
        //we already have a Global variable for array. don't need to create a new, we have 2 access items that we alrrady hav

        for (int r = 0; r < 8; r++)
        {
            for (int s = 0; s < 8; s++)
            {
                Debug.Log(Tiles[r, s].name); // this print the name of Gameobject at r, s index of Gameobject Array "Tiles".
                //now we need to call the SetType method for this particular Tile at r, s, but we don't have reference to the "Tile" component of the tile bai
                //we only hav GameObject reference. do u know the fix bai? getComponent yeeee. ur salary += 1000;
                //retype it

                //lets brek it down; we need a 'Tile' reference. lets create a local variable for it
                Tile targetTile = Tiles[r, s].GetComponent<Tile>();

                //at this point we should have tile ref in targetTile. get it? yres
                //           nao call SetType for targetTile here

                //enums are value type, classes are reference types. If you pass enum, you pass value, if you pass object, u pass the reference.
                //nao lets actully randomze the colors. All we need is to create a random number between 1-4.
                int rand = Random.Range(1, 5); //we use 5 max cuz if using ints, Random.Range max value is exclusive of the max u passed ok sux. naht sux
                //cuz most of the times u werking with arrays. and instead of typing array.Length -1 max, u just type array.lLength #VB #CSharpMasterRace

            //ok lets use the random\\
                targetTile.SetType((TileType)rand); //we pass the randomized value. nao it has errors. cuz we need to Convert it to 'TileType'
                //this is Explicit conversion. Excplicit is when u tell exactly wat to convert to. Implicit is when it does it automatically
                //implicit is in case of float to int, or int to float etc.k




            }
        }
        //we didnt call this method. u see hao it iterates and prints name? yee
        //bhai. wat., y u r knowing these tings? i r be werks in Unity and C# since 5 years bai. y u r knowing hao 2 maek tiles gaems?
        // i r thinks and solutions pop up in mind. also i hav made Tiles game before. naht match 3 tiles tho. look at Twenty48Soliatire

        //bai r u ready for nek?
        // did we set the colours? naht yet. i wuz be shows u hao 2 iterate thru arrays. i showed on 2d array, nao 1d array be piece of shitcake 4 u


    }


}

