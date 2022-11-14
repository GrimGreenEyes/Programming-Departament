using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static GridCreator instance;

    private GameObject[,] tileMap;
    private EnumMapOptions.mapBiom actualBiome;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private GameObject pathTilePrefab;
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
        //TeamInfo teamManager = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
        //for (int i = 0; i < teamManager.GetPlantsList().Count; i++)
        //{
        //    players.Add(teamManager.GetPlantsList()[i].GetPlantType());
        //    players[i].GetComponent<Plants>().SetStats(teamManager.GetPlantsList()[i].GetCurrentHP(), teamManager.GetPlantsList()[i].GetPlantHealth(), teamManager.GetPlantsList()[i].GetPlantAttack(), teamManager.GetPlantsList()[i].GetPlantDeffense(), teamManager.GetPlantsList()[i].GetPlantHeatRessistance(), teamManager.GetPlantsList()[i].GetPlantColdRessistance(), teamManager.GetPlantsList()[i].GetPlantAgility(), teamManager.GetPlantsList()[i].GetPlantMovement());
        //    players[i].GetComponent<Plants>().SetSkills(teamManager.GetPlantsList()[i].GetPlantSkills());
        //}
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
            }
        }
        GenerateRoad();
        GenerateEntitys(players, 1);
        GenerateEntitys(enemys, tileMap.GetLength(1) - 1);
        GameController.instance.OrderCharacters();
        
    }
    private void GenerateRoad() 
    {
        int roadRow = Random.Range(3, x - 3);
        for(int i = 0; i < y; i++)
        {

            Destroy(tileMap[roadRow, i]);
            GameObject pathTile = Instantiate(pathTilePrefab, transform, false);
            tileMap[roadRow, i] = pathTile;
            pathTile.transform.localPosition = new Vector3((i + roadRow) * 0.5f, (i - roadRow) * 0.25f, 0);
            pathTile.GetComponent<Tile>().Init(0, roadRow, i);
            pathTile.name = roadRow + ", " + i;
        }
    }
    private void GenerateEntitys(List<GameObject> entitys, int row)
    {
        int xEntity = row;
        int yEntity = 1;
        for (int playerArrayPos = 0; playerArrayPos < entitys.Count; playerArrayPos++)
        {
            yEntity = (tileMap[xEntity, yEntity].transform.childCount > 2) ? yEntity += 1 : yEntity;
            player = Instantiate(entitys[playerArrayPos], tileMap[xEntity, yEntity].transform.position + new Vector3(0, 0.25f, 0), new Quaternion(0, 0, 0, 0));
            
            GameController.instance.Init(player);
            yEntity += 2;
        }

    }
    private void createObstacles()
    {

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
        tileMap[(int)Random.Range(1, width - 1), (int)Random.Range(1, height - 1)].GetComponent<Tile>().GenerateSeed();
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