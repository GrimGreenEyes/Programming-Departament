using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static GridCreator instance;

    private GameObject[,] tileMap;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private GameObject rail;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemys;
    public float width, height;
    public int x, y;


    //public 

    public GameObject player;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        if (x < 20)
        {
            x = 20;
        }
        if (y < 19)
        {
            y = 19;
        }
        width = (x);
        height = (y);
        tileMap = new GameObject[x, y];
        Create();
    }
    private void Create()
    {
        int arrayPos = 0;
        x = 0;
        y = 0;
        for(float i = 0; i < width; x++, i++)
        {
            y = 0;
            for(float j = 0; j < height; y++, j++)
            {
                GameObject tile = Instantiate(tilePrefab, transform, false);
                tileMap[x, y] = tile;
                tile.transform.localPosition = new Vector3((j + x) * 0.5f, (j - x) * 0.25f , 0);
                tile.GetComponent<Tile>().Init((arrayPos++) % 2, x, y);
                tile.name = x + ", " + y;
                /*
                if (i == 0 || j == 0 || i == (width - tile.transform.localScale.x) || j == (height - tile.transform.localScale.y))
                {
                    int border = 0;
                    
                    if (i == 0 && j == 0)
                    {
                        border = 3;
                    }
                    else if (i == 0 && j == height - tilePrefab.transform.localScale.y)
                    {
                        border = 1;
                    }
                    else if (i == 0)
                    {
                        border = 8;
                    }
                    else if (i == width - tile.transform.localScale.x && j == height - tile.transform.localScale.y)
                    {
                        border = 2;
                    }
                    else if (j == height - tile.transform.localScale.y)
                    {
                        border = 5;
                    }
                    else if (i == width - tile.transform.localScale.x && j == 0)
                    {
                        border = 4;
                    }
                    else if (i == width - tile.transform.localScale.x)
                    {
                        border = 6;
                    }
                    else if (j == 0)
                    {
                        border = 7;
                    }


                    tile.layer = 3;
                    tile.GetComponent<Tile>().SetBorder(border);
                }
                */
                /*
                else if(Random.Range(0, 50) < 1)
                {
                    Instantiate(ObstaclePrefab, tile.transform.position , Quaternion.Euler(0, 0, 0), tile.transform);
                }
                else if (j > 4 && playerArrayPos < players.Length)
                {
                    player = Instantiate(players[playerArrayPos], tile.transform.position + new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));
                    GameController.instance.Init(player, playerArrayPos);
                    playerArrayPos++;
                }*/
            }
        }
        //GenerateRoad();
        GenerateEntitys(players, 1);
        GenerateEntitys(enemys, tileMap.GetLength(1) - 1);
        GameController.instance.OrderCharacters();
        
    }
    private void GenerateRoad() 
    {
        int roadRow = Random.Range(1, x);
        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                if(j == roadRow)
                {
                    Instantiate(rail, tileMap[i,j].transform, false);
                }
            }
        }
    }
    private void GenerateEntitys(GameObject[] entitys, int row)
    {
        int xEntity = row;
        int yEntity = 1;
        for (int playerArrayPos = 0; playerArrayPos < entitys.Length; playerArrayPos++)
        {
            yEntity = (tileMap[xEntity, yEntity].transform.childCount > 2) ? yEntity += 1 : yEntity;
            player = Instantiate(entitys[playerArrayPos], tileMap[xEntity, yEntity].transform.position + new Vector3(0, 0.25f, 0), new Quaternion(0, 0, 0, 0));
            
            GameController.instance.Init(player);
            yEntity += 2;
        }

    }
    public GameObject GetTile(int x, int y)
    {
        if(x < 0 || y < 0)
        {
            return null;
        }
        return tileMap[x, y];
    }
    private void createObstacles()
    {

    }
    private void Update()
    {
        //for (int i = 0; i < tileMap.GetLength(0); i++)
        //{
        //    for (int j = 0; j < tileMap.GetLength(1); j++)
        //    {
        //        tileMap[i, j].GetComponent<Tile>().StopShine();
        //    }
        //}
    }
    public void ShineTiles(int x, int y, int distance, bool setClickable)
    {
        int auxY = y;
        for(int i = x; i >= -distance + x; i--)
        {
            for(int j = y; j + i >= -distance + x + y; j--)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                tileMap[i, j].GetComponent<Tile>().SetClickable(setClickable);

                if(j == 1)
                {
                    j = -100;
                }
            }
            for (int j = y; j + i <= distance + x + auxY; j++)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                tileMap[i, j].GetComponent<Tile>().SetClickable(setClickable);
              
                if (j == tileMap.GetLength(1) - 2)
                {
                    j = tileMap.GetLength(1) + 100;
                }
            }
            if (i == 1)
            {
                i = -100;
            }
            auxY -= 2;
        }
        auxY = y;
        for(int i = x; i <= distance + x; i++)
        {
            for(int j = y; j + i <= distance + x + y; j++)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                tileMap[i, j].GetComponent<Tile>().SetClickable(setClickable);
                if(j == tileMap.GetLength(1) - 2)
                {
                    j = tileMap.GetLength(1) + 100;
                }
            }
            for (int j = y; j + i >= -distance + x + auxY; j--)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                tileMap[i, j].GetComponent<Tile>().SetClickable(setClickable);
                if (j == 1)
                {
                    j = -100;
                }
            }
            if (i == tileMap.GetLength(0) - 2)
            {
                i = tileMap.GetLength(0) + 100;
            }
            auxY += 2;
        }
    }
    public void SearchObjective(int x, int y, int distance, bool setClickable)
    {
        int auxY = y;
        for (int i = x; i >= -distance + x; i--)
        {
            for (int j = y; j + i >= -distance + x + y; j--)
            {
                if(tileMap[i, j].GetComponent<Tile>().entity != null)
                {
                    tileMap[i, j].GetComponent<Tile>().ShineEntity();
                }
                
                if (j == 1)
                {
                    j = -100;
                }
            }
            for (int j = y; j + i <= distance + x + auxY; j++)
            {
                if (tileMap[i, j].GetComponent<Tile>().entity != null)
                {
                    tileMap[i, j].GetComponent<Tile>().ShineEntity();
                }

                if (j == tileMap.GetLength(1) - 2)
                {
                    j = tileMap.GetLength(1) + 100;
                }
            }
            if (i == 1)
            {
                i = -100;
            }
            auxY -= 2;
        }
        auxY = y;
        for (int i = x; i <= distance + x; i++)
        {
            for (int j = y; j + i <= distance + x + y; j++)
            {
                if (tileMap[i, j].GetComponent<Tile>().entity != null)
                {
                    tileMap[i, j].GetComponent<Tile>().ShineEntity();
                }
                if (j == tileMap.GetLength(1) - 2)
                {
                    j = tileMap.GetLength(1) + 100;
                }
            }
            for (int j = y; j + i >= -distance + x + auxY; j--)
            {
                if (tileMap[i, j].GetComponent<Tile>().entity != null)
                {
                    tileMap[i, j].GetComponent<Tile>().ShineEntity();
                }
                if (j == 1)
                {
                    j = -100;
                }
            }
            if (i == tileMap.GetLength(0) - 2)
            {
                i = tileMap.GetLength(0) + 100;
            }
            auxY += 2;
        }
    }
}