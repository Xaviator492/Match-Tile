using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    

    public GameObject TilePrefab;  

    public Tile[,] Tiles; 

    public int Height;
    public int Width;

    public int MinMatches = 3;


    [Range(0.05f, 1f)] //define a range and unity converts it into a slider
    public float SwapDuration;

    [Range(0.05f, 1f)]
    public float MatchDuration;

    [Range(0.05f, 0.2f)]
    public float DropDuration;

    [Range(0.05f, 0.2f)]
    public float SpawnDuration;
    

    private int matchMultiplier = 1;

    private void Start()
    {

        Tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {

                Tile tile =  Instantiate(TilePrefab, transform).GetComponent<Tile>();
                tile.Pos = new Vector2(x, y);
                //this sets the reference of "tile" to the x, y index in the Tiles array
                Tiles[x, y] = tile;
                tile.name = string.Format("Tile ({0}, {1})", x, y);
                tile.transform.position = new Vector3(x, y, 0f); //Space them out based on the Y index the loop is in
            }


        }

        
        float camX = Width % 2 == 0 ? Width / 2f - 0.5f : Width / 2f;
        float camY = Height % 2 == 0 ? Height / 2f - 0.5f : Height / 2f;

        Camera.main.transform.position = new Vector3(camX, camY, -10f);


        RandomizeColours();
        
    }


    public void SwapTile(Tile target, Vector2 direction)
    {
        GameController.Instance.AllowInput = false;

        Vector2 tilePos = target.Pos;
        if (tilePos.x < 0 || tilePos.x > Width - 1 || tilePos.y < 0 || tilePos.y > Height - 1)
        {
            return;
        }

        Vector2 targetPos = tilePos + direction;
        if (targetPos.x < 0 || targetPos.x > Width - 1 || targetPos.y < 0 || targetPos.y > Height - 1)
        {
            return;
        }

        Tile otherTile = Tiles[(int)targetPos.x, (int)targetPos.y];
        otherTile.transform.DOMove(tilePos, SwapDuration);
        target.transform.DOMove(targetPos, SwapDuration).OnComplete(()=> 
        {
            
            Tiles[(int)target.Pos.x, (int)target.Pos.y] = otherTile;
            Tiles[(int)otherTile.Pos.x, (int)otherTile.Pos.y] = target;

            otherTile.Pos = tilePos;
            target.Pos = targetPos;
            StartCoroutine(CheckMatches());

        });
    }

    public IEnumerator CheckMatches()
    {

        List<Tile> tilesMatched = VerticalMatch();
        bool match = false;
        if (tilesMatched.Count > 0)
        {
            match = true;
            foreach (Tile t in tilesMatched)
            {
                t.Deactivate(MatchDuration);
            }
            yield return new WaitForSeconds(MatchDuration + 0.1f);
            StartCoroutine(CheckMatches());
        }
        else
        {
            tilesMatched = HorizontalMatch();
            if (tilesMatched.Count > 0)
            {
                match = true;
                foreach (Tile t in tilesMatched)
                {
                    t.Deactivate(MatchDuration);
                }
                yield return new WaitForSeconds(MatchDuration + 0.1f);
                StartCoroutine(CheckMatches());
            }

        }
        if (match)
        {
            StartCoroutine(ApplyGravity());
            yield return null; //note: IEnumerators must yield. 
        }
        else
        {
            bool spawned = false;
            for (int x = 0; x < Width; x++)
            {
                if (SpawnTile(x))
                {
                    spawned = true;
                }
            }
            if (spawned)
            {
                Invoke("StartCheckMatch", SpawnDuration * 1.2f);
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(SpawnDuration * 1.5f);
                GameController.Instance.AllowInput = true;
                matchMultiplier = 1;
                yield return null;
            }
        }
    }

    public List<Tile> VerticalMatch()
    {

        List<Tile> tilesMatched = new List<Tile>();
        bool matched = false;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height - 2; y++)
            {
                Tile tile = Tiles[x, y];
                Tile tile2 = Tiles[x, y + 1];
                Tile tile3 = Tiles[x, y + 2];



                if (tile != null && tile2 != null && tile3 != null
                    && tile.Type == tile2.Type && tile2.Type == tile3.Type)
                {
                    matched = true;
                    tilesMatched.Add(tile);
                    tilesMatched.Add(tile2);
                    tilesMatched.Add(tile3);
                    GameController.Instance.Score += 1 * matchMultiplier;
                    matchMultiplier++;
                    break;
                }
            }
            if (matched)
            {
                break;
            }
        }
        return tilesMatched;
         
    }

    public List<Tile> HorizontalMatch()
    {

        List<Tile> tilesMatched = new List<Tile>();
        bool matched = false;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width - 2; x++)
            {
                Tile tile = Tiles[x, y];
                Tile tile2 = Tiles[x + 1, y];
                Tile tile3 = Tiles[x + 2, y];
                if (tile != null && tile2 != null && tile3 != null
                    && tile.Type == tile2.Type && tile2.Type == tile3.Type)
                {
                    matched = true;
                    tilesMatched.Add(tile);
                    tilesMatched.Add(tile2);
                    tilesMatched.Add(tile3);
                    GameController.Instance.Score += 1 * matchMultiplier;
                    matchMultiplier++;
                    break;
                }
            }
            if (matched)
            {
                break;
            }
        }
        return tilesMatched;

    }

    public IEnumerator ApplyGravity()
    {
        bool fell = false;

        List<Tile> toDrop = new List<Tile>();

        for (int x = 0; x < Width; x++)
        {
            int steps = 0;
            for (int y = 1; y < Height; y++)
            {
                Tile current = Tiles[x, y];
                Tile below = Tiles[x, y - 1];

                if (current == null)
                {
                    continue;
                }

                if (below == null || !below.Activated)
                {
                    fell = true;
                    Tiles[x, y] = null;
                    current.Drop(1+steps, DropDuration);
                    current.Pos = new Vector2(x, y - (1+steps));
                    Tiles[x, y - (1+steps)] = current;
                    //steps++;
                    yield return new WaitForSeconds(DropDuration / 2f);
                }
                /*else
                {
                    if (steps > 0)
                    {
                        current.Drop(steps, DropDuration);
                        current.Pos = new Vector2(x, y - steps);
                        yield return new WaitForSeconds(DropDuration / 2f);
                    }

                }*/
            }
        }
        yield return new WaitForSeconds(DropDuration/2f);
        if (fell)
        {
            StartCoroutine(ApplyGravity());
        }
        else
        {
            StartCoroutine(CheckMatches());
            yield return null;
        }
    }

    public void StartCheckMatch()
    {
        StartCoroutine(CheckMatches());
    }

    public bool SpawnTile(int x)
    {
        bool spawned = false;
        for (int i = Height - 1; i > 0; i--)
        {
            if (Tiles[x, i] == null)
            {

                Tile tile = GameController.Instance.GetTile();

                if (tile == null)
                {
                    tile = Instantiate(TilePrefab, transform).GetComponent<Tile>();
                    Debug.Log("New Tile was instantiated");
                }

                //this sets the reference of "tile" to the x, y index in the Tiles array
                Tiles[x, i] = tile;
                tile.name = string.Format("Tile ({0}, {1})", x, i);
                tile.transform.position = new Vector3(x, i, 0f);
                tile.transform.localScale = Vector3.zero;
                tile.Activate(x, i, RandomTileType(), SpawnDuration);
                spawned = true;

            }
        }
        return spawned;
    }

    public TileType RandomTileType()
    {
        int rand = Random.Range(1, 6);
        return (TileType)rand;
    }


    public void RandomizeColours()
    {

        for (int r = 0; r < Width; r++)
        {
            for (int s = 0; s < Height; s++)
            {
                //Debug.Log(Tiles[r, s].name);
                Tile targetTile = Tiles[r, s].GetComponent<Tile>();
                
                int rand = Random.Range(1, 5);
                
                targetTile.SetType((TileType)rand); 




            }
        }
    }
}

