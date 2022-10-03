using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantsManager : MonoBehaviour
{
    private List<FlowerPot> potsList = new List<FlowerPot>();
    private List<Plant> plantsList = new List<Plant>();
    [SerializeField] Inventory inventoryManager;
    private Item plantingSeedItem;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private MsgWindow msgWindow;


    private void Start()
    {
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
        newPlant.transform.localPosition = new Vector3(0f, 185f, 0f);
        newPlant.GetComponent<Image>().color = plantType.color;
        plantsList.Add(newPlant.GetComponent<Plant>());
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
}
