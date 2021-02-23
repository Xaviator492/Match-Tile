using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    


    public GameObject TilePrefab;  //we declared this to make it appear on inspector so we can set  a reference to it later

    
    //below is an array of type GameObject
    public GameObject[,] Tiles; 

    

    private void Start()
    {

        Tiles = new GameObject[8, 8]; //2d array

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                #region Comments
                //nested loop to create on both x and y axis

                #endregion
                GameObject tile = Instantiate(TilePrefab, transform);
                tile.transform.parent = this.transform;
                //this sets the reference of "tile" to the x, y index in the Tiles array
                Tiles[x, y] = tile;

                tile.name = string.Format("Tile ({0}, {1})", x, y);
                tile.transform.position = new Vector3(x, y, 0f); //now we space them out based on the Y index the loop is in
            }


        }//loop(s) end here

       
        Camera.main.transform.position = new Vector3(3.5f, 3.5f, -10f);
        //centre the camera over the middle of the playing field. 7 spaces between the 8 tiles. Divide by 2 to centre.

        RandomizeColours();
        
    }


 
    public void RandomizeColours()
    {
      //this sets the colours randomly for each of the tile prefabs

        for (int r = 0; r < 8; r++)
        {
            for (int s = 0; s < 8; s++)
            {
                Debug.Log(Tiles[r, s].name); // this print the name of Gameobject at r, s index of Gameobject Array "Tiles".


                //local variable for Tile reference
                Tile targetTile = Tiles[r, s].GetComponent<Tile>();


                int rand = Random.Range(1, 6);

                targetTile.SetType((TileType)rand); //we pass the randomized value to the SertType function in Tile. 




            }
        }
       
    }

    public void SwapTile(int x, int y, int dir)
    {
        GameObject nextTile;
        GameObject thisTile;
        thisTile = Tiles[x, y];


        if (dir == 0)
        {
            //move right
            nextTile = Tiles[x + 1, y];
            nextTile.transform.position = new Vector2(x, y);
            Tiles[x + 1, y] = thisTile;
            Tiles[x, y] = nextTile;
        }
        else if (dir == 1)
        {
            //move left
            nextTile = Tiles[x - 1, y];
            nextTile.transform.position = new Vector2(x, y);
            Tiles[x - 1, y] = thisTile;
            Tiles[x, y] = nextTile;

        }
        else if (dir == 2)
        {
            //move up
            nextTile = Tiles[x, y + 1];
            nextTile.transform.position = new Vector2(x, y);
            Tiles[x, y + 1] = thisTile;
            Tiles[x, y] = nextTile;
        }
        else if (dir == 3)
        {
            //move down
            nextTile = Tiles[x, y - 1];
            nextTile.transform.position = new Vector2(x, y);
            Tiles[x, y - 1] = thisTile;
            Tiles[x, y] = nextTile;
        }
    }

}

