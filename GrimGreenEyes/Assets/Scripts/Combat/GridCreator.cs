using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private GameObject[] players;
    public float width;
    public float height;
    private float i = 0;
    private float j = 0;

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
        width = (width + 2) * tilePrefab.transform.localScale.x;
        height = (height + 2) * tilePrefab.transform.localScale.y;
        Create();
    }
    private void Create()
    {
        int playerArrayPos = 0;
        int arrayPos = 0;
        for(  i = 0; i < width;)
        {
            for(j = 0; j < height; )
            {
                GameObject tile = Instantiate(tilePrefab, transform, false);
                tile.transform.localPosition = new Vector3(i + 0.5f, j + 0.5f, 0);
                tile.GetComponent<Tile>().Init((arrayPos++) % 2);
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
}
