using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantsManager : MonoBehaviour
{
    private List<FlowerPot> potsList = new List<FlowerPot>();
    public Plant[] plantsList = new Plant[5];
    public Animator[] tapsAnimatorsList = new Animator[5];
    public Animator[] waterdropsAnimatorsList = new Animator[5];
    [SerializeField] Inventory inventoryManager;
    private Item plantingSeedItem;
    private Fertilizer usingFertilizer;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private MsgWindow msgWindow;
    [SerializeField] private WaterTank waterTank;
    private bool[] lockedTap = new bool[5];

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
        newPlant.transform.localPosition = new Vector3(5f, 50f, 0f);
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
        newPlant.transform.localPosition = new Vector3(5f, 50f, 0f);
        newPlant.GetComponent<Plant>().SetPlantType(plantType);
        potsList[pot].SetPlant(newPlant.GetComponent<Plant>());
        plantsList[potsList[pot].index] = newPlant.GetComponent<Plant>();
        potsList[pot].free = false;
        newPlant.GetComponent<Plant>().UpdatePlantVisuals();
    }

    public void FertilizePlant()
    {
        uiManager.ActivateAllElements();
        uiManager.PlaySoundSeed();
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
        if(plantsList[plantIndex] == null)
        {
            uiManager.ShowWarning("No hay ninguna planta bajo este grifo", 2);
            return;
        }
        if (plantsList[plantIndex].plantState >= 2 && plantsList[plantIndex].healthPoints >= plantsList[plantIndex].GetMaxHealth())
        {
            uiManager.ShowWarning("Esta planta tiene toda la salud", 2);
            return;
        }
        if (waterTank.waterAmount <= 0)
        {
            uiManager.ShowWarning("No te queda agua", 1.5f);
            return;
        }
        else
        {
            if (lockedTap[plantIndex])
            {
                return;
            }
            else
            {
                lockedTap[plantIndex] = true;
                waterTank.RemoveWater();
                uiManager.PlaySoundTap();
                uiManager.PlaySoundWater();
                tapsAnimatorsList[plantIndex].SetBool("openTab", true);
                waterdropsAnimatorsList[plantIndex].SetBool("dropWater", true);
                StartCoroutine(DelayWaterPlant(plantIndex));
            }
        }
    }

    private IEnumerator DelayWaterPlant(int index)
    {
        yield return new WaitForSeconds(1.2f);
        plantsList[index].ReceiveWater();
        tapsAnimatorsList[index].SetBool("openTab", false);
        waterdropsAnimatorsList[index].SetBool("dropWater", false);
        lockedTap[index] = false;
    }

    public bool AnyFreePot()
    {
        bool freePot = false;
        foreach(FlowerPot pot in potsList)
        {
            if(pot.free == true)
            {
                freePot = true;
            }
        }
        return freePot;
    }

    public bool CheckFertilizer(Fertilizer fertilizer)
    {
        bool canBeUsed = false;
        foreach(Plant plant in plantsList)
        {
            if(plant != null)
            {
                if(!plant.skillsList.Contains(fertilizer.skill) && plant.skillsList.Count < 3)
                {
                    canBeUsed = true;
                }
            }
        }
        return canBeUsed;
    }

    public void RemovePlant(Plant plant)
    {
        potsList[IndexOf(plantsList, plant)].free = true;
        plantsList[IndexOf(plantsList, plant)] = null;
        uiManager.RemoveButton(plant.GetComponent<Button>());
        Destroy(plant.gameObject);
    }

    private int IndexOf(Plant[] list, Plant item)
    {
        for(int i = 0; i < list.Length; i++)
        {
            if (list[i] == item) return i;
        }
        return -1;
    }
}
