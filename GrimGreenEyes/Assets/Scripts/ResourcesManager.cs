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

    public void Start()
    {
        teamInfo = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
        LoadResources();
    }

    public void StoreResources()
    {
        Debug.Log("Resources: STORE RESOURCES");
        teamInfo.itemsDictionary = inventoryManager.inventoryItems;
        teamInfo.fertilizersDictionary = fertilizerManager.storedFertilizers;
        teamInfo.waterAmount = waterTank.waterAmount;

        teamInfo.StorePlantsList(plantsManager.plantsList);
    }

    public void LoadResources()
    {
        Debug.Log("Resources: LOAD RESOURCES");
        inventoryManager.inventoryItems = teamInfo.itemsDictionary;
        fertilizerManager.storedFertilizers = teamInfo.fertilizersDictionary;
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
                j++;
                
            }
        }
    }

    public void loadScreen(string sceneName)
    {
        StoreResources();
        SceneManager.LoadScene(sceneName);
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
        }



    }
}
