using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public TeamInfo teamInfo;
    public Inventory inventoryManager;
    public PlantsManager plantsManager;
    public FertilizerManager fertilizerManager;
    public WaterTank waterTank;

    public void StoreResources()
    {
        Debug.Log("STORE RESOURCES");
        teamInfo.itemsDictionary = inventoryManager.inventoryItems;
        teamInfo.fertilizersDictionary = fertilizerManager.storedFertilizers;
        teamInfo.waterAmount = waterTank.waterAmount;
    }

    public void LoadResources()
    {
        Debug.Log("LOAD RESOURCES");
        inventoryManager.inventoryItems = teamInfo.itemsDictionary;
        fertilizerManager.storedFertilizers = teamInfo.fertilizersDictionary;
        waterTank.waterAmount = teamInfo.waterAmount;
    }
}
