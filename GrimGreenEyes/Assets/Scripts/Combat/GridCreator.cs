using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static GridCreator instance;

    private GameObject[,] tileMap;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private GameObject[] players;
    private float width, height;
    public int x, y;

    private GameObject player;

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
        width = (x + 2) * tilePrefab.transform.localScale.x;
        height = (y + 2) * tilePrefab.transform.localScale.y;
        tileMap = new GameObject[x + 2, y + 2];
        Create();
    }
    private void Create()
    {
        int playerArrayPos = 0;
        int arrayPos = 0;
        x = 0;
        y = 0;
        for(float i = 0; i < width; x++)
        {
            y = 0;
            for(float j = 0; j < height; y++)
            {
                GameObject tile = Instantiate(tilePrefab, transform, false);
                tileMap[x, y] = tile;
                tile.transform.localPosition = new Vector3(i + 0.5f, j + 0.5f, 0);
                tile.GetComponent<Tile>().Init((arrayPos++) % 2, x, y);
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
                else if(Random.Range(0, 50) < 1)
                {
                    Instantiate(ObstaclePrefab, tile.transform.position , Quaternion.Euler(0, 0, 0), tile.transform);
                }
                else if (j > 4 && playerArrayPos < players.Length)
                {
                    player = Instantiate(players[playerArrayPos], tile.transform.position + new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));
                    GameController.instance.Init(player, playerArrayPos);
                    playerArrayPos++;
                }
                j = j + tilePrefab.transform.localScale.y;
            }
            i = i + tilePrefab.transform.localScale.x;
        }
    }
    private void Update()
    {
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                tileMap[i, j].GetComponent<Tile>().StopShine();
            }
        }
    }
    public void ShineTiles(int x, int y, int distance)
    {
        int auxY = y;
        for(int i = x; i >= -distance + x; i--)
        {
            for(int j = y; j + i >= -distance + x + y; j--)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                if(j == 1)
                {
                    j = -6;
                }
            }
            for (int j = y; j + i <= distance + x + auxY; j++)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                if (j == tileMap.GetLength(1) - 2)
                {
                    j = tileMap.GetLength(1) + 6;
                }
            }
            if (i == 1)
            {
                i = -6;
            }

            auxY -= 2;
        }
        auxY = y;
        for(int i = x; i <= distance + x; i++)
        {
            for(int j = y; j + i <= distance + x + y; j++)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                if(j == tileMap.GetLength(1) - 2)
                {
                    j = tileMap.GetLength(1) + 6;
                }
            }
            for (int j = y; j + i >= -distance + x + auxY; j--)
            {
                tileMap[i, j].GetComponent<Tile>().ShineTile();
                if (j == 1)
                {
                    j = -6;
                }
            }
            if (i == tileMap.GetLength(0) - 2)
            {
                i = tileMap.GetLength(0) + 6;
            }
            auxY += 2;
        }
    }
}
