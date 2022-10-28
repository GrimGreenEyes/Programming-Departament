using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantsManager : MonoBehaviour
{
    private List<FlowerPot> potsList = new List<FlowerPot>();
    public Plant[] plantsList = new Plant[5];
    [SerializeField] Inventory inventoryManager;
    private Item plantingSeedItem;
    private Fertilizer usingFertilizer;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private MsgWindow msgWindow;
    [SerializeField] private WaterTank waterTank;

    private void Start()
    {
        plantsList = new Plant[5];
        foreach (Transform child in transform)
        {
            potsList.Add(child.GetComponent<FlowerPot>());
        }
    }

    public void PlantSeed(Item item)
    {
        uiManager.DesactivateAllElements();
        plantingSeedItem = item;
        foreach(FlowerPot pot in potsList)
        {
            pot.Planting(item.seedType);
        }
        msgWindow.ShowPlantingMsg(item.seedType);
    }

    public void PlantSeedInPot(PlantType plantType, FlowerPot chosenPot)
    {
        uiManager.ActivateAllElements();
        foreach (FlowerPot pot in potsList)
        {
            pot.StopPlanting();
        }
        inventoryManager.RemoveItem(plantingSeedItem);

        GameObject newPlant = Instantiate(plantPrefab);
        newPlant.transform.SetParent(chosenPot.gameObject.transform);
        newPlant.transform.localScale = new Vector3(1f, 1f, 1f);
        newPlant.transform.localPosition = new Vector3(0f, 225f, 0f);
        newPlant.GetComponent<Plant>().SetPlantType(plantType);
        chosenPot.SetPlant(newPlant.GetComponent<Plant>());
        plantsList[chosenPot.index] = newPlant.GetComponent<Plant>();

        msgWindow.gameObject.SetActive(false);
        
    }

    public void LoadPlant(PlantType plantType, int pot)
    {
        GameObject newPlant = Instantiate(plantPrefab);
        newPlant.transform.SetParent(potsList[pot].transform);
        newPlant.transform.localScale = new Vector3(1f, 1f, 1f);
        newPlant.transform.localPosition = new Vector3(0f, 225f, 0f);
        newPlant.GetComponent<Plant>().SetPlantType(plantType);
        potsList[pot].SetPlant(newPlant.GetComponent<Plant>());
        plantsList[potsList[pot].index] = newPlant.GetComponent<Plant>();
        potsList[pot].free = false;
    }

    public void FertilizePlant()
    {
        uiManager.ActivateAllElements();
        foreach (FlowerPot pot in potsList)
        {
            pot.StopFertilizing();
        }
        msgWindow.gameObject.SetActive(false);
    }

    public void CancelPlanting()
    {
        uiManager.ActivateAllElements();
        foreach (FlowerPot pot in potsList)
        {
            pot.StopPlanting();
        }
    }

    public void UseFertilizer(Fertilizer fertilizer)
    {
        uiManager.DesactivateAllElements();
        usingFertilizer = fertilizer;
        foreach (FlowerPot pot in potsList)
        {
            pot.Fertilizing(fertilizer);
        }
        msgWindow.ShowFertilizingMsg(fertilizer);

    }

    public void CancelFertilizing()
    {
        uiManager.ActivateAllElements();
        foreach (FlowerPot pot in potsList)
        {
            pot.StopFertilizing();
        }
    }

    public void WaterPlant(int plantIndex)
    {
        if (plantsList[plantIndex] == null || waterTank.waterAmount <= 0 || (plantsList[plantIndex].plantState >= 2 && plantsList[plantIndex].healthPoints >= plantsList[plantIndex].GetMaxHealth()))
        {
            return;
        }
        else
        {
            plantsList[plantIndex].ReceiveWater();
            waterTank.RemoveWater();
        }
    }
}
