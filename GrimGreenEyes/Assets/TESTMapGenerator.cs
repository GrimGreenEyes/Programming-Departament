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
    [Header("GRID")]
    [SerializeField] private int sizeX = 11;
    [SerializeField] private int sizeY = 14;
    private float distanceX = 1;
    private float distanceY = 1;

    [Header("PATH")]
    [SerializeField] private int minPathOriginY = 3;
    [SerializeField] private int maxPathOriginY = 10;
    [SerializeField] private int minPathEndY = 3;
    [SerializeField] private int maxPathEndY = 10;
    [SerializeField] private int minPathDistanceY = 2;
    [SerializeField] private int maxPathDistanceY = 5;

    [Header("WATER")]
    [Range(0,100)]
    [SerializeField] private float waterChance = 80;
    [SerializeField] private int minWaterX = 2;
    [Range(0, 100)]
    [SerializeField] private float averageStreamIntensity = 90;
    [Range(0, 100)]
    [SerializeField] private float averageStreamDecay = 40;
    [Range(0, 10)]
    [SerializeField] private int maxStreamSize = 3;

    private void Start()
    {
        squaresArray = new GameObject[sizeX, sizeY];
    }

    public void GenerateGrid()
    {
        DestroyCurrentGrid();
        GenerateBasicGround();
        GeneratePath();
        GenerateWater();
    }

    public void InstantiateSquare(int x, int y, Color color)
    {
        InstantiateSquareInternal(x, y, color, false);
    }

    public void InstantiateSquareInternal(int x, int y, Color color, bool overwrite)
    {
        if (squaresArray[x, y] != null)
        {
            if (squaresArray[x, y].GetComponent<SpriteRenderer>().color == Color.green || overwrite)
            {
                Destroy(squaresArray[x, y]);
            }
            else
            {
                return;
            }
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
            squaresArray = new GameObject[sizeX, sizeY];
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

    public void GenerateWater()
    {
       if(RandomInRange(1, 100) > waterChance)
        {
            return;
        }

       int waterOriginX = 0;
       int waterOriginY = 0;

        waterOriginX = RandomInRange(minWaterX, sizeX-3);
       waterOriginY = FindPathY(waterOriginX);
        waterOriginY = RandomInRange(waterOriginY - 2, waterOriginY + 2);

       InstantiateWater(waterOriginX, waterOriginY);
       InstantiateWater(waterOriginX + 1, waterOriginY);
       CreateWaterStream(waterOriginX, waterOriginY + 1, averageStreamIntensity, averageStreamDecay);
       CreateWaterStream(waterOriginX + 1, waterOriginY + 1, averageStreamIntensity, averageStreamDecay);
       CreateWaterStream(waterOriginX + 2, waterOriginY, averageStreamIntensity, averageStreamDecay);
       CreateWaterStream(waterOriginX - 1, waterOriginY, averageStreamIntensity, averageStreamDecay);
       CreateWaterStream(waterOriginX, waterOriginY - 1, averageStreamIntensity, averageStreamDecay);
       CreateWaterStream(waterOriginX + 1, waterOriginY - 1, averageStreamIntensity, averageStreamDecay);
        FillBorders(Color.green, Color.blue);
        FillBorders(Color.blue, Color.green);
    }

    public void InstantiateWater(int x, int y)
    {
        if (x >= sizeX || x < 0 || y >= sizeY || y < 0) return;
        InstantiateSquare(x, y, Color.blue);
    }

    public void InstantiateSquareOverwrite(int x, int y, Color color)
    {
        InstantiateSquareInternal(x, y, color, true);
    }

    public void CreateWaterStream(int x, int y, float intensity, float decay)
    {
        if (x >= sizeX || x < 0 || y >= sizeY || y < 0) return;
        //Crea líneas de agua

        //Primero la dirección que puede ser hacia alante o hacia atrás
        int waterDirection = 1;
        if(RandomInRange(1, 100) >= 50)
        {
            waterDirection = -1;
        }

        float chances = intensity;
        bool generateNext = true;
        int currentX = x;
        int currentY = y;
        int currentSize = 1;
        while (generateNext)
        {
            if(currentX >= sizeX || currentX < 0 || currentY >= sizeY || currentY <= 0)
            {
                return;
            }
            if(RandomInRange(1, 100) > chances || currentX < minWaterX || currentSize > maxStreamSize)
            {
                generateNext = false;
            }
            else
            {
                InstantiateWater(currentX, currentY);
                currentX += waterDirection;
                currentY++;
                chances -= decay;
                currentSize++;
            }
        }
    }

    public void FillBorders(Color originalColor, Color fixedColor)
    {
        for(int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                if (squaresArray[i, j].GetComponent<SpriteRenderer>().color == originalColor)
                {
                    bool north = false;
                    bool west = false;
                    bool south = false;
                    bool east = false;

                    if (j + 1 >= sizeY)
                    {
                        north = true;
                    }
                    else if (squaresArray[i, j + 1].GetComponent<SpriteRenderer>().color != originalColor)
                    {
                        north = true;
                    }

                    if (i - 1 < 0)
                    {
                        west = true;
                    }
                    else if (squaresArray[i - 1, j].GetComponent<SpriteRenderer>().color != originalColor)
                    {
                        west = true;
                    }

                    if (j - 1 < 0)
                    {
                        south = true;
                    }
                    else if (squaresArray[i, j - 1].GetComponent<SpriteRenderer>().color != originalColor)
                    {
                        south = true;
                    }

                    if (i + 1 >= sizeX)
                    {
                        east = true;
                    }
                    else if (squaresArray[i + 1, j].GetComponent<SpriteRenderer>().color != originalColor)
                    {
                        east = true;
                    }

                    if (north && west && south && east)
                    {
                        //InstantiateWater(i, j);
                        InstantiateSquareOverwrite(i, j, fixedColor);
                    }
                }
            }
        }
    }

    private int FindPathY(int x)
    {
        int coordY = 0;
        for(int i = 0; i < sizeY; i++)
        {
            if(squaresArray[x, i].GetComponent<SpriteRenderer>().color == Color.red)
            {
                coordY = i;
            }
        }
        return coordY;
    }

    public int RandomInRange(int min, int max)
    {
        return random.Next(min, max + 1);
    }
}
