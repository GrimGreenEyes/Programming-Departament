using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class GridCreator : MonoBehaviour
{
    public Biome desert, forest, snow, plains;
    public Biome currentBiome;


    //public GameObject square;
    public GameObject origin;
    //private GameObject button;
    //private GameObject[,] squaresArray;

    System.Random random = new System.Random();

    public Camera camera;

    //private Dictionary<string, GameObject> tilesDictionary = new Dictionary<string, GameObject>();
    //[SerializeField] private GameObject groundTile, pathTile, waterTile, mountainTile, powerupTile, healerTile, damagerTile, eventTile;

    //PARAMETTERS
    [Header("GRID")]
    [SerializeField] private int sizeX = 11;
    [SerializeField] private int sizeY = 14;
    private float distanceX = 0.5f;
    private float distanceY = 0.25f;

    [Header("PATH")]
    [SerializeField] private int minPathOriginX = 3;
    [SerializeField] private int maxPathOriginX = 10;
    [SerializeField] private int minPathEndX = 3;
    [SerializeField] private int maxPathEndX = 10;
    [SerializeField] private int minPathDistanceX = 2;
    [SerializeField] private int maxPathDistanceX = 5;

    private bool specialEvent = false;
    private bool itsRaining = false;

    /*private void Start()
    {
        squaresArray = new GameObject[sizeX, sizeY];
        //button = GameObject.Find("GenerateButton");
        
    }*/
    private void Start()
    {
        //InitializeBiomeValues();
        if (x < 10)
        {
            x = 10;
        }
        if (y < 9)
        {
            y = 9;
        }
        width = (x);
        height = (y);
        tileMap = new GameObject[x, y];
        GenerateGrid();
        //Create();
    }

    public void GenerateGrid()
    {
        DestroyCurrentGrid();

        InitializeBiomeValues();
        GetRandomEvent();

        GenerateBasicGround();
        GeneratePath();
        GenerateSpawnBlocks();
        GenerateWater();
        GenerateMountains();
        GeneratePowerups();
        GenerateHealers();
        GenerateDamagers();
        //GenerateEventTiles();


        WateringTiles();
        GenerateEntitys(players);
        GenerateEntitys(enemys);
        GenerateEntitys(carriage);
        GameController.instance.OrderCharacters();
    }
    
    public void InstantiateSquare(int x, int y, string type)
    {
        InstantiateSquareInternal(x, y, type, false);
    }

    public void InstantiateSquareInternal(int x, int y, string type, bool overwrite)
    {
        if (x >= sizeX || x < 0 || y >= sizeY || y < 0) return;
        if (tileMap[x, y] != null)
        {
            if ((tileMap[x, y].GetComponent<Tile>().type == "ground" && !tileMap[x, y].GetComponent<Tile>().spawnBlocks) || overwrite)
            {
                Destroy(tileMap[x, y]);
            }
            else
            {
                return;
            }
        }
        GameObject aux = Instantiate(currentBiome.tilesDictionary[type]);
        aux.transform.SetParent(origin.transform);
        //aux.transform.localPosition = new Vector3(x * distanceX, y * distanceY, 0);
        aux.transform.localPosition = new Vector3((y + x) * 0.5f, (y - x) * 0.25f, 0);
        aux.name = x + ", " + y;
        aux.GetComponent<Tile>().Init((x + y) % 2, x, y);
        //aux.GetComponent<SpriteRenderer>().color = color;
        tileMap[x, y] = aux;
    }
    
    public void DestroyCurrentGrid()
    {
        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().specialEvent = false;
        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().specialEvent = false;

        specialEvent = false;
        itsRaining = false;

        foreach (Transform child in origin.transform)
        {
            Destroy(child.gameObject);
            tileMap = new GameObject[sizeX, sizeY];
        }
    }
    
    public void GenerateBasicGround()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                InstantiateSquare(i, j, "ground");
            }
        }
    }
    
    public void GeneratePath()
    {
        Debug.ClearDeveloperConsole();
        //Origin
        int originPathX = RandomInRange(minPathOriginX, maxPathOriginX);
        InstantiateSquare(originPathX, 0, "path");

        //End
        int endPathX = 0;
        do
        {
            endPathX = RandomInRange(System.Math.Clamp(originPathX - maxPathDistanceX, minPathEndX, maxPathEndX), Math.Clamp(originPathX + maxPathDistanceX, minPathEndX, maxPathEndX));
        } while (endPathX < originPathX + minPathDistanceX && endPathX > originPathX - 2);
        InstantiateSquare(endPathX - 1, sizeY - 1, "path");

        //Squares where path goes up
        int heightChangesNum = 0;
        bool isGoingUp; //Else is going down

        if (originPathX > endPathX)
        {
            isGoingUp = false;
        }
        else
        {
            isGoingUp = true;
        }

        if (!isGoingUp)
        {
            heightChangesNum = originPathX - endPathX + 1;
        }
        else
        {
            heightChangesNum = endPathX - originPathX - 1;
        }

        var heightChangesArray = Enumerable.Range(1, sizeY - 2).OrderBy(t => random.Next()).Take(heightChangesNum).ToArray();

        //Rest of the path
        int currentHeight = originPathX;
        for (int i = 1; i < sizeY - 1; i++)
        {
            InstantiateSquare(currentHeight, i, "path");
            if (heightChangesArray.Contains<int>(i))
            {
                if (isGoingUp)
                {
                    InstantiateSquareOverwrite(currentHeight + 1, i, "lPath3");
                    InstantiateSquareOverwrite(currentHeight, i, "lPath2");
                    currentHeight++;
                }
                else
                {
                    InstantiateSquareOverwrite(currentHeight - 1, i, "lPath1");
                    InstantiateSquareOverwrite(currentHeight, i, "lPath4");
                    currentHeight--;
                }
            }
        }
    }
    
    public void GenerateWater()
    {
        if (RandomInRange(1, 100) > currentBiome.waterChance)
        {
            return;
        }

        int waterOriginX = 0;
        int waterOriginY = 0;

        //waterOriginX = RandomInRange(minWaterX, sizeX - 3);
        //waterOriginY = FindPathY(waterOriginX);
        //waterOriginY = RandomInRange(waterOriginY - 2, waterOriginY + 2);

        waterOriginY = RandomInRange(currentBiome.minWaterY, sizeY - 3);
        waterOriginX = FindPathX(waterOriginY);
        waterOriginX = RandomInRange(waterOriginX - 2, waterOriginX + 2);

        InstantiateWater(waterOriginX, waterOriginY);
        InstantiateWater(waterOriginX + 1, waterOriginY);
        CreateWaterStream(waterOriginX, waterOriginY + 1, currentBiome.averageStreamIntensity, currentBiome.averageStreamDecay);
        CreateWaterStream(waterOriginX + 1, waterOriginY + 1, currentBiome.averageStreamIntensity, currentBiome.averageStreamDecay);
        CreateWaterStream(waterOriginX + 2, waterOriginY, currentBiome.averageStreamIntensity, currentBiome.averageStreamDecay);
        CreateWaterStream(waterOriginX - 1, waterOriginY, currentBiome.averageStreamIntensity, currentBiome.averageStreamDecay);
        CreateWaterStream(waterOriginX, waterOriginY - 1, currentBiome.averageStreamIntensity, currentBiome.averageStreamDecay);
        CreateWaterStream(waterOriginX + 1, waterOriginY - 1, currentBiome.averageStreamIntensity, currentBiome.averageStreamDecay);
        FillBorders("ground", "water");
        FillBorders("water", "ground");
    }
    
    public void InstantiateWater(int x, int y)
    {
        if (x >= sizeX || x < 0 || y >= sizeY || y < 0) return;
        InstantiateSquare(x, y, "water");
    }

    public void InstantiateSquareOverwrite(int x, int y, String type)
    {
        InstantiateSquareInternal(x, y, type, true);
    }

    public void CreateWaterStream(int x, int y, float intensity, float decay)
    {
        if (x >= sizeX || x < 0 || y >= sizeY || y < 0) return;
        //Crea líneas de agua

        //Primero la dirección que puede ser hacia alante o hacia atrás
        int waterDirection = 1;
        if (RandomInRange(1, 100) >= 50)
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
            if (currentX >= sizeX || currentX < 0 || currentY >= sizeY || currentY <= 0)
            {
                return;
            }
            if (RandomInRange(1, 100) > chances || currentY < currentBiome.minWaterY || currentSize > currentBiome.maxStreamSize)
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

    public void FillBorders(string originalType, string fixedType)
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (tileMap[i, j].GetComponent<Tile>().type == originalType)
                {
                    bool north = false;
                    bool west = false;
                    bool south = false;
                    bool east = false;

                    if (j + 1 >= sizeY)
                    {
                        north = true;
                    }
                    else if (tileMap[i, j + 1].GetComponent<Tile>().type != originalType)
                    {
                        north = true;
                    }

                    if (i - 1 < 0)
                    {
                        west = true;
                    }
                    else if (tileMap[i - 1, j].GetComponent<Tile>().type != originalType)
                    {
                        west = true;
                    }

                    if (j - 1 < 0)
                    {
                        south = true;
                    }
                    else if (tileMap[i, j - 1].GetComponent<Tile>().type != originalType)
                    {
                        south = true;
                    }

                    if (i + 1 >= sizeX)
                    {
                        east = true;
                    }
                    else if (tileMap[i + 1, j].GetComponent<Tile>().type != originalType)
                    {
                        east = true;
                    }

                    if (north && west && south && east)
                    {
                        //InstantiateWater(i, j);
                        InstantiateSquareOverwrite(i, j, fixedType);
                    }
                }
            }
        }
    }

    public void FillBordersTarget(string originalType, string fixedType)
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (tileMap[i, j].GetComponent<Tile>().type == originalType)
                {
                    bool north = false;
                    bool west = false;
                    bool south = false;
                    bool east = false;

                    if (j + 1 >= sizeY)
                    {
                        north = true;
                    }
                    else if (tileMap[i, j + 1].GetComponent<Tile>().type == fixedType)
                    {
                        north = true;
                    }

                    if (i - 1 < 0)
                    {
                        west = true;
                    }
                    else if (tileMap[i - 1, j].GetComponent<Tile>().type == fixedType)
                    {
                        west = true;
                    }

                    if (j - 1 < 0)
                    {
                        south = true;
                    }
                    else if (tileMap[i, j - 1].GetComponent<Tile>().type == fixedType)
                    {
                        south = true;
                    }

                    if (i + 1 >= sizeX)
                    {
                        east = true;
                    }
                    else if (tileMap[i + 1, j].GetComponent<Tile>().type == fixedType)
                    {
                        east = true;
                    }

                    if (north && west && south && east)
                    {
                        //InstantiateWater(i, j);
                        InstantiateSquareOverwrite(i, j, fixedType);
                    }
                }
            }
        }
    }

    private int FindPathX(int y)
    {
        int coordX = 0;
        for (int i = 0; i < sizeX; i++)
        {
            if (tileMap[i, y].GetComponent<Tile>().type == "path")
            {
                coordX = i;
            }
        }
        return coordX;
    }
    
    private void GenerateMountains()
    {
        if (RandomInRange(1, 100) > currentBiome.anyMountainChance) return;

        int numOfMountains = 0;
        numOfMountains = RandomInRange(3, 7);

        for (int i = 0; i < numOfMountains; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;
            bool stop = false;
            int tries = 0;
            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if (!AnySquareInRange(randomX, randomY, "water", 4) && !AnySquareInRange(randomX, randomY, "mountain", 4))
                {
                    if (tileMap[randomX, randomY].GetComponent<Tile>().type == "ground")
                    {
                        foundPosition = true;
                    }
                }
                tries++;
                if (tries > 300)
                {
                    stop = true;
                }

            } while (!foundPosition && !stop);

            if (foundPosition)
            {
                InstantiateSquare(randomX, randomY, "mountain");
                if (RandomInRange(1, 100) < currentBiome.mountainGrowthChance && randomX + 1 < sizeX) InstantiateSquare(randomX + 1, randomY, "mountain");
                if (RandomInRange(1, 100) < currentBiome.mountainGrowthChance && randomX - 1 >= 0) InstantiateSquare(randomX - 1, randomY, "mountain");
                if (RandomInRange(1, 100) < currentBiome.mountainGrowthChance && randomY + 1 < sizeY) InstantiateSquare(randomX, randomY + 1, "mountain");
                if (RandomInRange(1, 100) < currentBiome.mountainGrowthChance && randomY - 1 >= 0) InstantiateSquare(randomX, randomY - 1, "mountain");
            }

        }

        FillBordersTarget("ground", "mountain");
    }

    private bool AnySquareInRange(int originX, int originY, string type, int range)
    {
        //range--;
        for (int i = originX - range; i < originX + range; i++)
        {
            for (int j = originY - range; j < originY + range; j++)
            {
                if (i + j < originX + originY + range)
                {
                    if (i >= 0 && i < sizeX && j > 0 && j < sizeY)
                    {
                        if (tileMap[i, j].GetComponent<Tile>().type == type)
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
        int pathOriginX = FindPathX(0);
        Debug.Log("ORIGEN: " +pathOriginX);

        tileMap[pathOriginX + 1, 0].GetComponent<Tile>().spawnBlocks = true;
        tileMap[pathOriginX - 1, 0].GetComponent<Tile>().spawnBlocks = true;
        tileMap[pathOriginX + 2, 0].GetComponent<Tile>().spawnBlocks = true;
        if (tileMap[pathOriginX + 1, 1].GetComponent<Tile>().type == "path")
        {
            tileMap[pathOriginX + 2, 1].GetComponent<Tile>().spawnBlocks = true;
        }
        else
        {
            tileMap[pathOriginX + 1, 1].GetComponent<Tile>().spawnBlocks = true;
        }

        if (tileMap[pathOriginX - 1, 1].GetComponent<Tile>().type == "path")
        {
            tileMap[pathOriginX - 2, 1].GetComponent<Tile>().spawnBlocks = true;
        }
        else
        {
            tileMap[pathOriginX - 1, 1].GetComponent<Tile>().spawnBlocks = true;
        }

        foreach (GameObject tile in tileMap)
        {
            if (tile.GetComponent<Tile>().spawnBlocks)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        
    }
    
    private void GeneratePowerups()
    {
        if (RandomInRange(1, 100) > currentBiome.anyPowerupChance) return;

        int numberOfPowerups = RandomInRange(currentBiome.minPowerups, currentBiome.maxPowerups);
        for (int i = 0; i < numberOfPowerups; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;

            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if (tileMap[randomX, randomY].GetComponent<Tile>().type == "ground" && AnySquareInRange(randomX, randomY, "path", 4))
                {
                    foundPosition = true;
                }

            } while (!foundPosition);

            InstantiateSquare(randomX, randomY, "powerup");
            if (randomX + 1 < sizeX) InstantiateSquare(randomX + 1, randomY, "powerup");

            if (randomX - 1 >= 0 && RandomInRange(1, 100) < currentBiome.powerupGrowthChance) InstantiateSquare(randomX - 1, randomY, "powerup");
            if (randomX + 2 >= 0 && RandomInRange(1, 100) < currentBiome.powerupGrowthChance) InstantiateSquare(randomX + 2, randomY, "powerup");

            if (randomY + 1 < sizeY && RandomInRange(1, 100) < currentBiome.powerupGrowthChance) InstantiateSquare(randomX, randomY + 1, "powerup");
            if (randomX + 1 < sizeX && randomY + 1 < sizeY && RandomInRange(1, 100) < currentBiome.powerupGrowthChance) InstantiateSquare(randomX + 1, randomY + 1, "powerup");

            if (randomY - 1 >= 0 && RandomInRange(1, 100) < currentBiome.powerupGrowthChance) InstantiateSquare(randomX, randomY - 1, "powerup");
            if (randomX + 1 < sizeX && randomY - 1 >= 0 && RandomInRange(1, 100) < currentBiome.powerupGrowthChance) InstantiateSquare(randomX + 1, randomY - 1, "powerup");
        }
    }

    private void GenerateHealers()
    {
        if (RandomInRange(1, 100) > currentBiome.anyHealerChance) return;

        int numberOfHealers = RandomInRange(currentBiome.minHealers, currentBiome.maxHealers);
        for (int i = 0; i < numberOfHealers; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;

            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if (tileMap[randomX, randomY].GetComponent<Tile>().type == "ground" && AnySquareInRange(randomX, randomY, "path", 5))
                {
                    foundPosition = true;
                }

            } while (!foundPosition);

            InstantiateSquare(randomX, randomY, "healer");

            if (RandomInRange(1, 100) > 50) //Vertical
            {
                InstantiateSquare(randomX + 1, randomY, "healer");
                if (RandomInRange(1, 100) < currentBiome.healerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY + 1, "healer");
                    InstantiateSquare(randomX + 1, randomY + 1, "healer");
                    if (RandomInRange(1, 100) < currentBiome.healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY + 2, "healer");
                        InstantiateSquare(randomX + 1, randomY + 2, "healer");
                    }
                }
                if (RandomInRange(1, 100) < currentBiome.healerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY - 1, "healer");
                    InstantiateSquare(randomX + 1, randomY - 1, "healer");
                    if (RandomInRange(1, 100) < currentBiome.healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY - 2, "healer");
                        InstantiateSquare(randomX + 1, randomY - 2, "healer");
                    }
                }
            }
            else //Horizontal
            {
                InstantiateSquare(randomX, randomY - 1, "healer");
                if (RandomInRange(1, 100) < currentBiome.healerGrowthChance)
                {
                    InstantiateSquare(randomX + 1, randomY, "healer");
                    InstantiateSquare(randomX + 1, randomY - 1, "healer");
                    if (RandomInRange(1, 100) < currentBiome.healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX + 2, randomY, "healer");
                        InstantiateSquare(randomX + 2, randomY - 1, "healer");
                    }
                }
                if (RandomInRange(1, 100) < currentBiome.healerGrowthChance)
                {
                    InstantiateSquare(randomX - 1, randomY, "healer");
                    InstantiateSquare(randomX - 1, randomY - 1, "healer");
                    if (RandomInRange(1, 100) < currentBiome.healerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX - 2, randomY, "healer");
                        InstantiateSquare(randomX - 2, randomY - 1, "healer");
                    }
                }
            }
        }
    }

    private void GenerateDamagers()
    {
        if (RandomInRange(1, 100) > currentBiome.anyDamagerChance) return;

        int numberOfDamagers = RandomInRange(currentBiome.minDamagers, currentBiome.maxDamagers);
        for (int i = 0; i < numberOfDamagers; i++)
        {
            int randomX, randomY;
            bool foundPosition = false;

            do
            {

                randomX = RandomInRange(0, sizeX - 1);
                randomY = RandomInRange(0, sizeY - 1);
                if (tileMap[randomX, randomY].GetComponent<Tile>().type == "path" && AnySquareInRange(randomX, randomY, "path", 5))
                {
                    foundPosition = true;
                }

            } while (!foundPosition);

            InstantiateSquare(randomX, randomY, "damager");

            if (RandomInRange(1, 100) > 50) //Vertical
            {
                InstantiateSquare(randomX + 1, randomY, "damager");
                if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY + 1, "damager");
                    InstantiateSquare(randomX + 1, randomY + 1, "damager");
                    if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY + 2, "damager");
                        InstantiateSquare(randomX + 1, randomY + 2, "damager");
                    }
                }
                if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance)
                {
                    InstantiateSquare(randomX, randomY - 1, "damager");
                    InstantiateSquare(randomX + 1, randomY - 1, "damager");
                    if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX, randomY - 2, "damager");
                        InstantiateSquare(randomX + 1, randomY - 2, "damager");
                    }
                }
            }
            else //Horizontal
            {
                InstantiateSquare(randomX, randomY - 1, "damager");
                if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance)
                {
                    InstantiateSquare(randomX + 1, randomY, "damager");
                    InstantiateSquare(randomX + 1, randomY - 1, "damager");
                    if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX + 2, randomY, "damager");
                        InstantiateSquare(randomX + 2, randomY - 1, "damager");
                    }
                }
                if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance)
                {
                    InstantiateSquare(randomX - 1, randomY, "damager");
                    InstantiateSquare(randomX - 1, randomY - 1, "damager");
                    if (RandomInRange(1, 100) < currentBiome.damagerGrowthChance / 2)
                    {
                        InstantiateSquare(randomX - 2, randomY, "damager");
                        InstantiateSquare(randomX - 2, randomY - 1, "damager");
                    }
                }
            }
        }
    }

    private void GenerateEventTiles()
    {
        if (!specialEvent) return;

        switch (currentBiome.name)
        {
            case "desert":
                Debug.Log("EVENTO: Tormenta de arena");
                break;
            case "snow":
                Debug.Log("EVENTO: Tormenta de nieve");
                break;
        }

        ReplaceAllBlocks("ground", "event");
    }

    private void ReplaceAllBlocks(string typeOld, string typeNew)
    {
        for(int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                if(tileMap[i, j].GetComponent<Tile>().type == typeOld)
                {
                    InstantiateSquareOverwrite(i, j, typeNew);
                }
            }
        }
    }
    bool first = true;
    private void InitializeBiomeValues()
    {
        switch (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomaActual)
        {
            case EnumMapOptions.mapBiom.desierto:
                currentBiome = desert;
            break;
            case EnumMapOptions.mapBiom.llanura:
                currentBiome = plains;
            break;
            case EnumMapOptions.mapBiom.selva:
                currentBiome = forest;
            break;
            case EnumMapOptions.mapBiom.nieve:
                currentBiome = snow;
            break;
        }

        camera.backgroundColor = currentBiome.backgroundColor;

        if (first)
        {
            currentBiome.tilesDictionary.Add("ground", currentBiome.groundTile);
            currentBiome.tilesDictionary.Add("path", currentBiome.pathTile);
            currentBiome.tilesDictionary.Add("water", currentBiome.waterTile);
            currentBiome.tilesDictionary.Add("mountain", currentBiome.mountainTile);
            currentBiome.tilesDictionary.Add("powerup", currentBiome.powerupTile);
            currentBiome.tilesDictionary.Add("healer", currentBiome.healerTile);
            currentBiome.tilesDictionary.Add("damager", currentBiome.damagerTile);
            currentBiome.tilesDictionary.Add("event", currentBiome.eventTile);
            currentBiome.tilesDictionary.Add("lPath1", currentBiome.lPath1);
            currentBiome.tilesDictionary.Add("lPath2", currentBiome.lPath2);
            currentBiome.tilesDictionary.Add("lPath3", currentBiome.lPath3);
            currentBiome.tilesDictionary.Add("lPath4", currentBiome.lPath4);

            first = false;
        }
        
    }

    private void GetRandomEvent()
    {
        //Calcula si hay tormenta de arena o tormenta de nieve. Si no hay ninguna de las dos, entonces calcula si llueve o no
        if (RandomInRange(1, 100) < currentBiome.eventChance)
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().specialEvent = true;
            specialEvent = true;
        }
        else if (RandomInRange(1, 100) < currentBiome.rainChance)
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().itsRaining = true;
            itsRaining = true;
            Debug.Log("EVENTO: Lluvia");
        }
    }

    public int RandomInRange(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    ///////////////////////////////////////////////////////////////////////
    
    public static GridCreator instance;
    
    private GameObject[,] tileMap;
    private EnumMapOptions.mapBiom actualBiome;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private GameObject pathTilePrefab;
    [SerializeField] private List<GameObject> carriage;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private List<GameObject> enemys;
    public float width, height;
    public int x, y;

    public GameObject player;

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        TeamInfo teamManager = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
        for (int i = 0; i < teamManager.GetPlantsList().Count; i++)
        {   
            if(teamManager.GetPlantsList()[i].plantState == 2)
            {
                players.Add(teamManager.GetPlantsList()[i].GetPlantType());
                players[i].GetComponent<Plants>().SetStats(teamManager.GetPlantsList()[i].GetCurrentHP(), teamManager.GetPlantsList()[i].GetPlantHealth(), teamManager.GetPlantsList()[i].GetPlantAttack(), teamManager.GetPlantsList()[i].GetPlantDeffense(), teamManager.GetPlantsList()[i].GetPlantHeatRessistance(), teamManager.GetPlantsList()[i].GetPlantColdRessistance(), teamManager.GetPlantsList()[i].GetPlantAgility(), teamManager.GetPlantsList()[i].GetPlantMovement());
                players[i].GetComponent<Plants>().SetSkills(teamManager.GetPlantsList()[i].GetPlantSkills());
            }
        }
    }
    
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
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
            }
        }
        //GenerateRoad();
        GameController.instance.OrderCharacters();
    }
    private void GenerateEntitys(List<GameObject> entitys)
    {
        int playerArrayPos = 0;
        switch (entitys[playerArrayPos].tag)
        {
            case "Player":
                for(int i = 0; i < sizeX; i++)
                {
                    for(int j = 0; j < sizeY; j++)
                    {
                        if (tileMap[i, j].GetComponent<Tile>().spawnBlocks && playerArrayPos < entitys.Count())
                        {
                            player = Instantiate(entitys[playerArrayPos], tileMap[i, j].transform.position + new Vector3(0, 0.25f, 0), new Quaternion(0, 0, 0, 0));
                            playerArrayPos++;
                            GameController.instance.Init(player);
                        }
                    }
                }
                break;
            case "Enemy":
                for(int i = 1; i < sizeX; i++)
                {
                    if (i % 2 == 0 && playerArrayPos < entitys.Count() && tileMap[i, sizeY - 1].GetComponent<Tile>().entity == null && tileMap[i, sizeY - 1].GetComponent<Tile>().isWalkable)
                    {
                        player = Instantiate(entitys[playerArrayPos], tileMap[i, sizeY - 1].transform.position + new Vector3(0, 0.25f, 0), new Quaternion(0, 0, 0, 0));
                        playerArrayPos++;
                        GameController.instance.Init(player);
                    }
                }
                break;
            case "Carriage":
                for(int i = 0; i < sizeX; i++)
                {
                    if(tileMap[i, 0].tag == "PathTile")
                    {
                        player = Instantiate(entitys[playerArrayPos], tileMap[i, 0].transform.position + new Vector3(0, 0.25f, 0), new Quaternion(0, 0, 0, 0));
                        playerArrayPos++;
                        GameController.instance.Init(player);
                    }
                }
                break;
        }
        //int xEntity = row;
        //int yEntity = 1;
        //for (int playerArrayPos = 0; playerArrayPos < entitys.Count; playerArrayPos++)
        //{
        //    yEntity = (tileMap[xEntity, yEntity].transform.childCount > 2) ? yEntity += 1 : yEntity;
        //    player = Instantiate(entitys[playerArrayPos], tileMap[xEntity, yEntity].transform.position + new Vector3(0, 0.25f, 0), new Quaternion(0, 0, 0, 0));
        //    
        //    GameController.instance.Init(player);
        //    yEntity += 2;
        //}
    }
    public GameObject GetTile(int x, int y)
    {
        if(x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }

        return tileMap[x, y];
    }
    
    public void GenerateSeed()
    {
        //tileMap[(int)Random.Range(1, width - 1), (int)Random.Range(1, height - 1)].GetComponent<Tile>().GenerateSeed();
    }
    private void WateringTiles()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                tileMap[i, j].GetComponent<Tile>().GetWater();
            }
        }
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

            }
            for (int j = y; j + i <= distance + x + auxY; j++)
            {
                if (tileMap[i, j].GetComponent<Tile>().entity != null)
                {
                    tileMap[i, j].GetComponent<Tile>().ShineEntity();
                }
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

            }
            for (int j = y; j + i >= -distance + x + auxY; j--)
            {
                if (tileMap[i, j].GetComponent<Tile>().entity != null)
                {
                    tileMap[i, j].GetComponent<Tile>().ShineEntity();
                }

            }

            auxY += 2;
        }
    }
}

[System.Serializable]
public class Biome
{
    public string name;

    public Color backgroundColor;

    public Dictionary<string, GameObject> tilesDictionary = new Dictionary<string, GameObject>();
    [SerializeField] public GameObject groundTile, pathTile, waterTile, mountainTile, powerupTile, healerTile, damagerTile, eventTile, lPath1, lPath2, lPath3, lPath4;

    [Header("WATER")]
    [Range(0, 100)] public float waterChance = 80;
    public int minWaterY = 2;
    [Range(0, 100)] public float averageStreamIntensity = 90;
    [Range(0, 100)] public float averageStreamDecay = 40;
    [Range(0, 10)] public int maxStreamSize = 3;

    [Header("MOUNTAINS")]
    [Range(0, 100)] public int anyMountainChance = 100;
    public int minMountains = 3;
    public int maxMountains = 7;
    [Range(0, 100)] public int mountainGrowthChance = 50;

    [Header("POWERUPS")]
    [Range(0, 100)] public int anyPowerupChance = 50;
    public int minPowerups = 2;
    public int maxPowerups = 3;
    [Range(0, 100)] public int powerupGrowthChance = 50;

    [Header("HEALERS")]
    [Range(0, 100)] public int anyHealerChance = 70;
    public int minHealers = 4;
    public int maxHealers = 6;
    [Range(0, 100)] public int healerGrowthChance = 50;

    [Header("DAMAGERS")]
    [Range(0, 100)] public int anyDamagerChance = 70;
    public int minDamagers = 4;
    public int maxDamagers = 6;
    [Range(0, 100)] public int damagerGrowthChance = 50;

    [Header("EVENT")]
    [Range(0, 100)] public int rainChance;
    [Range(0, 100)] public int eventChance;
}