using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourcesManager : MonoBehaviour
{
    public TeamInfo teamInfo;
    public Inventory inventoryManager;
    public PlantsManager plantsManager;
    public FertilizerManager fertilizerManager;
    public WaterTank waterTank;
    private GlobalVar globalVar;
    public Book book;

    public void Start()
    {
        teamInfo = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
        LoadResources();

        globalVar = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>();
        if (globalVar.firstTimeResources)
        {
            book.OpenBook();
            globalVar.firstTimeResources = false;
        }
    }

    public void StoreResources()
    {
        teamInfo.itemsDictionary = inventoryManager.inventoryItems;
        teamInfo.fertilizersDictionary = fertilizerManager.storedFertilizers;
        teamInfo.waterAmount = waterTank.waterAmount;

        teamInfo.StorePlantsList(plantsManager.plantsList);
    }

    public void LoadResources()
    {
        inventoryManager.inventoryItems = teamInfo.itemsDictionary;
        fertilizerManager.storedFertilizers = teamInfo.fertilizersDictionary;
        if (teamInfo.waterAmount > waterTank.maxWater) teamInfo.waterAmount = waterTank.maxWater;
        
        waterTank.waterAmount = teamInfo.waterAmount;

        waterTank.UpdateWater();

        LoadPlantsList();
    }

    private List<PlantInfo> plantInfoList;

    private void LoadPlantsList()
    {
        plantInfoList = teamInfo.plantsList;
        int i = 0;
        List<int> indexList = new List<int>();
        foreach(PlantInfo newPlant in plantInfoList)
        {
            if (newPlant != null)
            {
                if (newPlant.GetCurrentHP() > 0)
                {
                    plantsManager.LoadPlant(newPlant.plantTypeInternal, i);
                    i++;
                }
                else
                {
                    //teamInfo.plantsList.Remove(newPlant);
                    indexList.Add(i);
                }
            }
        }

        for(int x = indexList.Count - 1; x >= 0; x--)
        {
            plantInfoList.RemoveAt(x);
        }

        int j = 0;
        foreach(Plant plant in plantsManager.plantsList)
        {
            if (plant != null)
            {
                plant.healthPoints = teamInfo.plantsList[j].GetCurrentHP();
                plant.plantState = teamInfo.plantsList[j].plantState;
                plant.skillsList = teamInfo.plantsList[j].skillsInternal;
                plant.statsDictionary = teamInfo.plantsList[j].statsDictionary;
                plant.plantState = teamInfo.plantsList[j].plantState;
                plant.UpdatePlantVisuals();
                j++;
                
            }
        }
    }

    public void loadScreen(string sceneName)
    {
        StoreResources();/*
        GameObject.Find("GlobalAttributes").GetComponent<MainController>().loadScreen(sceneName);
        //SceneManager.LoadScene(sceneName);
        if (sceneName == "MapScene" & GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().isLastNode)
        {
            sceneName = "LoadMapScene";
            GameObject.Find("GlobalAttributes").GetComponent<MainController>().loadScreen(sceneName);
        }
            
        if (string.Equals(sceneName, "MapScene"))
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.returnMap);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);

        }
        else if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().isMapUp == true)
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(false);
        }

        if (string.Equals(sceneName, "MainScene"))
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchLoose);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(false);
        }*/
       // if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().isLastNode)
         //   sceneName = "LoadMapScene";
        GameObject.Find("GlobalAttributes").GetComponent<MainController>().loadScreen(sceneName);



    }
}
