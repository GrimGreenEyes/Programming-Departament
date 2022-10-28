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
    }

    public void LoadResources()
    {
        Debug.Log("Resources: LOAD RESOURCES");
        inventoryManager.inventoryItems = teamInfo.itemsDictionary;
        fertilizerManager.storedFertilizers = teamInfo.fertilizersDictionary;
        waterTank.waterAmount = teamInfo.waterAmount;

        waterTank.UpdateWater();
    }

    public void loadScreen(string sceneName)
    {
        StoreResources();
        SceneManager.LoadScene(sceneName);
        if (string.Equals(sceneName, "MapScene"))
        {
            Debug.Log("IGUAL SCENEMAE");
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
