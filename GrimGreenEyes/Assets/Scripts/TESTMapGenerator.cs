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
    private GameObject button;

    private GameObject[,] squaresArray;

    System.Random random = new System.Random();
    private Color lime = new Color(0.749f, 1f, 0f, 1f);
    private Color purple = new Color(0.8f, 0f, 1f, 1f);
    private Color orange = new Color(0.8f, 0.3f, 0f, 1f);
    private Color pink = new Color(0.9f, 0.7f, 1f, 1f);

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

    [Header("MOUNTAINS")]
    [Range(0, 100)]
    [SerializeField] private int anyMountainChance = 100;
    [SerializeField] private int minMountains = 3;
    [SerializeField] private int maxMountains = 7;
    [Range(0, 100)]
    [SerializeField] private int mountainGrowthChance = 50;

    [Header("POWERUPS")]
    [Range(0, 100)]
    [SerializeField] private int anyPowerupChance = 50;
    [SerializeField] private int minPowerups = 2;
    [SerializeField] private int maxPowerups = 3;
    [Range(0, 100)]
    [SerializeField] private int powerupGrowthChance = 50;

    [Header("HEALERS")]
    [Range(0, 100)]
    [SerializeField] private int anyHealerChance = 70;
    [SerializeField] private int minHealers = 4;
    [SerializeField] private int maxHealers = 6;
    [Range(0, 100)]
    [SerializeField] private int healerGrowthChance = 50;

    [Header("DAMAGERS")]
    [Range(0, 100)]
    [SerializeField] private int anyDamagerChance = 70;
    [SerializeField] private int minDamagers = 4;
    [SerializeField] private int maxDamagers = 6;
    [Range(0, 100)]
    [SerializeField] private int damagerGrowthChance = 50;

    private void Start()
    {
        squaresArray = new GameObject[sizeX, sizeY];
        button = GameObject.Find("GenerateButton");
    }

    public void GenerateGrid()
    {
        LockButton();

        DestroyCurrentGrid();
        GenerateBasicGround();
        GeneratePath();
        GenerateSpawnBlocks();
        GenerateWater();
        GenerateMountains();
        GeneratePowerups();
        GenerateHealers();
        GenerateDamagers();

        UnlockButton();
    }

    public void InstantiateSquare(int x, int y, Color color)
    {
        InstantiateSquareInternal(x, y, color, false);
    }

    public void InstantiateSquareInternal(int x, int y, Color color, bool overwrite)
    {
        if (x >= sizeX || x < 0 || y >= sizeY || y < 0) return;
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

    public void FillBordersTarget(Color originalColor, Color fixedColor)
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
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
                    else if (squaresArray[i, j + 1].GetComponent<SpriteRenderer>().color == fixedColor)
                    {
                        north = true;
                    }

                    if (i - 1 < 0)
                    {
                        west = true;
                    }
                    else if (squaresArray[i - 1, j].GetComponent<SpriteRenderer>().color == fixedColor)
                    {
                        west = true;
                    }

                    if (j - 1 < 0)
                    {
                        south = true;
                    }
                    else if (squaresArray[i, j - 1].GetComponent<SpriteRenderer>().color == fixedColor)
                    {
                        south = true;
                    }

                    if (i + 1 >= sizeX)
                    {
                        east = true;
                    }
                    else if (squaresArray[i + 1, j].GetComponent<SpriteRenderer>().color == fixedColor)
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

    private void GenerateMountains()
    {
        if (RandomInRange(1, 100) > anyMountainChance) return;

        int numOfMountains = 0;
        numOfMountains = RandomInRange(3, 7);

        for(int i = 0; i < numOfMountains; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;
            bool stop = false;
            int tries = 0;
            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if(!AnySquareInRange(randomX, randomY, Color.blue, 4) && !AnySquareInRange(randomX, randomY, Color.grey, 4))
                {
                    if (squaresArray[randomX, randomY].GetComponent<SpriteRenderer>().color == Color.green)
                    {
                        foundPosition = true;
                    }
                }
                tries++;
                if (tries > 300) stop = true;

            } while (!foundPosition && !stop);

            if(foundPosition){
                InstantiateSquare(randomX, randomY, Color.gray);
                if (RandomInRange(1, 100) < mountainGrowthChance && randomX + 1 < sizeX) InstantiateSquare(randomX + 1, randomY, Color.gray);
                if (RandomInRange(1, 100) < mountainGrowthChance && randomX - 1 >= 0) InstantiateSquare(randomX - 1, randomY, Color.gray);
                if (RandomInRange(1, 100) < mountainGrowthChance && randomY + 1 < sizeY) InstantiateSquare(randomX, randomY + 1, Color.gray);
                if (RandomInRange(1, 100) < mountainGrowthChance && randomY - 1 >= 0) InstantiateSquare(randomX, randomY - 1, Color.gray);
            }

        }

        FillBordersTarget(Color.green, Color.gray);
    }

    private bool AnySquareInRange(int originX, int originY, Color color, int range)
    {
        //range--;
        for(int i = originX - range; i < originX + range; i++)
        {
            for(int j = originY - range; j < originY + range; j++)
            {
                if (i + j < originX + originY + range)
                {
                    if (i >= 0 && i < sizeX && j > 0 && j < sizeY)
                    {
                        if(squaresArray[i,j].GetComponent<SpriteRenderer>().color == color)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private void GenerateSpawnBlocks()
    {
        int pathOriginY = FindPathY(0);

        InstantiateSquare(0, pathOriginY + 1, lime);
        InstantiateSquare(0, pathOriginY - 1, lime);
        InstantiateSquare(0, pathOriginY + 2, lime);
        if(squaresArray[1, pathOriginY+1].GetComponent<SpriteRenderer>().color == Color.red)
        {
            InstantiateSquare(1, pathOriginY + 2, lime);
        }
        else
        {
            InstantiateSquare(1, pathOriginY + 1, lime);
        }

        if (squaresArray[1, pathOriginY - 1].GetComponent<SpriteRenderer>().color == Color.red)
        {
            InstantiateSquare(1, pathOriginY - 2, lime);
        }
        else
        {
            InstantiateSquare(1, pathOriginY - 1, lime);
        }
    }

    private void GeneratePowerups()
    {
        if (RandomInRange(1, 100) > anyPowerupChance) return;

        int numberOfPowerups = RandomInRange(minPowerups, maxPowerups);
        for(int i = 0; i < numberOfPowerups; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;

            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                    if (squaresArray[randomX, randomY].GetComponent<SpriteRenderer>().color == Color.green && AnySquareInRange(randomX, randomY, Color.red, 4))
                    {
                        foundPosition = true;
                    }

            } while (!foundPosition);

            InstantiateSquare(randomX, randomY, purple);
            if (randomX + 1 < sizeX) InstantiateSquare(randomX+1, randomY, purple);

            if (randomX - 1 >= 0 && RandomInRange(1, 100) < powerupGrowthChance) InstantiateSquare(randomX-1, randomY, purple);
            if (randomX + 2 >= 0 && RandomInRange(1, 100) < powerupGrowthChance) InstantiateSquare(randomX + 2, randomY, purple);

            if (randomY + 1 < sizeY && RandomInRange(1, 100) < powerupGrowthChance) InstantiateSquare(randomX, randomY + 1, purple);
            if (randomX + 1 < sizeX && randomY + 1 < sizeY && RandomInRange(1, 100) < powerupGrowthChance) InstantiateSquare(randomX + 1, randomY + 1, purple);

            if (randomY - 1 >= 0 && RandomInRange(1, 100) < powerupGrowthChance) InstantiateSquare(randomX, randomY - 1, purple);
            if (randomX + 1 < sizeX && randomY - 1 >= 0 && RandomInRange(1, 100) < powerupGrowthChance) InstantiateSquare(randomX + 1, randomY - 1, purple);
        }
    }

    private void GenerateHealers()
    {
        if (RandomInRange(1, 100) > anyHealerChance) return;

        int numberOfHealers = RandomInRange(minHealers, maxHealers);
        for (int i = 0; i < numberOfHealers; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;

            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if (squaresArray[randomX, randomY].GetComponent<SpriteRenderer>().color == Color.green && AnySquareInRange(randomX, randomY, Color.red, 5))
                {
                    foundPosition = true;
                }

            } while (!foundPosition);

            InstantiateSquare(randomX, randomY, pink);

            if(RandomInRange(1, 100) > 50) //Vertical
            {
                InstantiateSquare(randomX+1, randomY, pink);
                if(RandomInRange(1, 100) < healerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY + 1, pink);
                    InstantiateSquare(randomX + 1, randomY + 1, pink);
                    if(RandomInRange(1, 100) < healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY + 2, pink);
                        InstantiateSquare(randomX + 1, randomY + 2, pink);
                    }
                }
                if (RandomInRange(1, 100) < healerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY - 1, pink);
                    InstantiateSquare(randomX + 1, randomY - 1, pink);
                    if (RandomInRange(1, 100) < healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY - 2, pink);
                        InstantiateSquare(randomX + 1, randomY - 2, pink);
                    }
                }
            }
            else //Horizontal
            {
                InstantiateSquare(randomX, randomY - 1, pink);
                if (RandomInRange(1, 100) < healerGrowthChance)
                {
                    InstantiateSquare(randomX+1, randomY, pink);
                    InstantiateSquare(randomX+1, randomY-1, pink);
                    if (RandomInRange(1, 100) < healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX+2, randomY, pink);
                        InstantiateSquare(randomX+2, randomY-1, pink);
                    }
                }
                if (RandomInRange(1, 100) < healerGrowthChance)
                {
                    InstantiateSquare(randomX - 1, randomY, pink);
                    InstantiateSquare(randomX - 1, randomY - 1, pink);
                    if (RandomInRange(1, 100) < healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX - 2, randomY, pink);
                        InstantiateSquare(randomX - 2, randomY - 1, pink);
                    }
                }
            }
        }
    }

    private void GenerateDamagers()
    {
        if (RandomInRange(1, 100) > anyDamagerChance) return;

        int numberOfDamagers = RandomInRange(minDamagers, maxDamagers);
        for (int i = 0; i < numberOfDamagers; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;

            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if (squaresArray[randomX, randomY].GetComponent<SpriteRenderer>().color == Color.green && AnySquareInRange(randomX, randomY, Color.red, 5))
                {
                    foundPosition = true;
                }

            } while (!foundPosition);

            InstantiateSquare(randomX, randomY, orange);

            if (RandomInRange(1, 100) > 50) //Vertical
            {
                InstantiateSquare(randomX + 1, randomY, orange);
                if (RandomInRange(1, 100) < damagerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY + 1, orange);
                    InstantiateSquare(randomX + 1, randomY + 1, orange);
                    if (RandomInRange(1, 100) < damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY + 2, orange);
                        InstantiateSquare(randomX + 1, randomY + 2, orange);
                    }
                }
                if (RandomInRange(1, 100) < damagerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY - 1, orange);
                    InstantiateSquare(randomX + 1, randomY - 1, orange);
                    if (RandomInRange(1, 100) < damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY - 2, orange);
                        InstantiateSquare(randomX + 1, randomY - 2, orange);
                    }
                }
            }
            else //Horizontal
            {
                InstantiateSquare(randomX, randomY - 1, orange);
                if (RandomInRange(1, 100) < damagerGrowthChance)
                {
                    InstantiateSquare(randomX + 1, randomY, orange);
                    InstantiateSquare(randomX + 1, randomY - 1, orange);
                    if (RandomInRange(1, 100) < damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX + 2, randomY, orange);
                        InstantiateSquare(randomX + 2, randomY - 1, orange);
                    }
                }
                if (RandomInRange(1, 100) < damagerGrowthChance)
                {
                    InstantiateSquare(randomX - 1, randomY, orange);
                    InstantiateSquare(randomX - 1, randomY - 1, orange);
                    if (RandomInRange(1, 100) < damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX - 2, randomY, orange);
                        InstantiateSquare(randomX - 2, randomY - 1, orange);
                    }
                }
            }
        }
    }

    private void LockButton()
    {
        button.GetComponent<Button>().interactable = false;
    }

    private void UnlockButton()
    {
        button.GetComponent<Button>().interactable = true;
    }

    public int RandomInRange(int min, int max)
    {
        return random.Next(min, max + 1);
    }
}
