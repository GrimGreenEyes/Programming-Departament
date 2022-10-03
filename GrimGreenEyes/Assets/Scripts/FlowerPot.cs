using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerPot : MonoBehaviour
{
    [SerializeField] private PlantsManager plantsManager;
    [SerializeField] private GameObject choosingBorder;
    private PlantType choosingPlant;
    private PlantType currentPlant;
    private bool free = true;

    public void Start()
    {
        plantsManager = GameObject.Find("FlowerPotsGroup").GetComponent<PlantsManager>();
        GetComponent<Button>().interactable = false;
    }

    public void Planting(PlantType plantType)
    {
        if (!free) return;

        GetComponent<Button>().interactable = true;
        choosingBorder.SetActive(true);
        choosingPlant = plantType;
    }

    public void StopPlanting()
    {
        GetComponent<Button>().interactable = false;
        choosingBorder.SetActive(false);
    }

    public void ChoosePot()
    {
        plantsManager.PlantSeedInPot(choosingPlant, this);
        free = false;
    }
}
