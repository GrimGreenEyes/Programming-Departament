using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private GameObject[] players;
    public int width;
    public int height;
    private GameObject player;


    private void Start()
    {
        if (width < 20)
        {
            width = 20;
        }
        if (height < 19)
        {
            height = 19;
        }
        width = width + 2;
        height = height + 2;
        Create();
    }
    private void Create()
    {
        int playerArrayPos = 0;
        int arrayPos = 0;
        for( float i = 0; i < width; i++)
        {
            for(float j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(TilePrefab, transform, false);
                tile.transform.localPosition = new Vector3(i + 0.5f, j + 0.5f, 0);
                tile.GetComponent<Tile>().Init((arrayPos++) % 2);
                if (i == 0 || j == 0 || i == (width-1) || j == (height - 1))
                {
                    int border = 0;
                    
                    if (i == 0 && j == 0)
                    {
                        border = 3;
                    }
                    else if (i == 0 && j == height - 1)
                    {
                        border = 1;
                    }
                    else if (i == 0)
                    {
                        border = 8;
                    }
                    else if (i == width - 1 && j == height - 1)
                    {
                        border = 2;
                    }
                    else if (j == height - 1)
                    {
                        border = 5;
                    }
                    else if (i == width - 1 && j == 0)
                    {
                        border = 4;
                    }
                    else if (i == width - 1)
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
            }
        }
    }
}
