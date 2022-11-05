using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class TESTMapGenerator : MonoBehaviour
{
    public GameObject square;
    public GameObject origin;

    private GameObject[,] squaresArray;

    System.Random random = new System.Random();

    //PARAMETTERS
    [SerializeField] private int sizeX = 11;
    [SerializeField] private int sizeY = 14;
    [SerializeField] private float distanceX = 1;
    [SerializeField] private float distanceY = 1;

    [SerializeField] private int minPathOriginY = 3;
    [SerializeField] private int maxPathOriginY = 10;
    [SerializeField] private int minPathEndY = 3;
    [SerializeField] private int maxPathEndY = 10;
    [SerializeField] private int minPathDistanceY = 2;
    [SerializeField] private int maxPathDistanceY = 5;

    private void Start()
    {
        squaresArray = new GameObject[sizeX, sizeY];
    }

    public void GenerateGrid()
    {
        DestroyCurrentGrid();
        GenerateBasicGround();
        GeneratePath();
    }

    public void InstantiateSquare(int x, int y, Color color)
    {
        if(squaresArray[x,y] != null)
        {
            Destroy(squaresArray[x, y]);
        }
        GameObject aux = Instantiate(square);
        aux.transform.SetParent(origin.transform);
        aux.transform.localPosition = new Vector3(x * distanceX, y * distanceY, 0);
        aux.GetComponent<SpriteRenderer>().color = color;
        squaresArray[x, y] = aux;
    }

    public void DestroyCurrentGrid()
    {
        foreach (Transform child in origin.transform)
        {
            Destroy(child.gameObject);
            squaresArray = new GameObject[11, 14];
        }
    }

    public void GenerateBasicGround()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                InstantiateSquare(i, j, Color.green);
            }
        }
    }

    public void GeneratePath()
    {
        Debug.ClearDeveloperConsole();
        //Origin
        int originPathY = RandomInRange(minPathOriginY, maxPathOriginY);
        InstantiateSquare(0, originPathY, Color.red);

        //End
        int endPathY = 0;
        do
        {
            endPathY = RandomInRange(Math.Clamp(originPathY - maxPathDistanceY, minPathEndY, maxPathEndY), Math.Clamp(originPathY + maxPathDistanceY, minPathEndY, maxPathEndY));
        } while (endPathY < originPathY + minPathDistanceY && endPathY > originPathY - 2);
        InstantiateSquare(sizeX - 1, endPathY, Color.red);

        //Squares where path goes up
        int heightChangesNum = 0;
        bool isGoingUp; //Else is going down

        if(originPathY > endPathY)
        {
            isGoingUp = false;
        }
        else
        {
            isGoingUp = true;
        }

        if (!isGoingUp)
        {
            heightChangesNum = originPathY - endPathY;
        }
        else
        {
            heightChangesNum = endPathY - originPathY;
        }

        var heightChangesArray = Enumerable.Range(1, sizeX - 2).OrderBy(t => random.Next()).Take(heightChangesNum).ToArray();

        //Rest of the path
        int currentHeight = originPathY;
        for(int i = 1; i < sizeX - 1; i++)
        {
            InstantiateSquare(i, currentHeight, Color.red);
            if (heightChangesArray.Contains<int>(i))
            {
                if (isGoingUp)
                {
                    InstantiateSquare(i, currentHeight + 1, Color.red);
                    currentHeight++;
                }
                else
                {
                    InstantiateSquare(i, currentHeight - 1, Color.red);
                    currentHeight--;
                }
            }
        }
    }

    public int RandomInRange(int min, int max)
    {
        return random.Next(min, max);
    }
}
