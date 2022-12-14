using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlowerPot : MonoBehaviour
{
    public int index;
    [SerializeField] private PlantsManager plantsManager;
    [SerializeField] private GameObject choosingBorder;
    private PlantType choosingPlant;
    private Plant currentPlant;
    public bool free = true;
    private int mode = 0; //PLANTING = 1; FERTILIZING = 2;
    private Fertilizer currentFertilizer = null;
    private FertilizerManager fertilizerManager;
    [SerializeField] private Sprite emptyPotSprite, usedPotSprite;
    [SerializeField] private Image potImage;
    public FlowerPotCollider potCollider;

    public void Start()
    {
        plantsManager = GameObject.Find("FlowerPotsGroup").GetComponent<PlantsManager>();
        fertilizerManager = GameObject.Find("Fertilizers").GetComponent<FertilizerManager>();
        GetComponent<Button>().interactable = false;
    }

    public void Planting(PlantType plantType)
    {
        mode = 1;
        if (!free) return;

        GetComponent<Button>().interactable = true;
        choosingBorder.SetActive(true);
        potCollider.gameObject.SetActive(true);
        choosingPlant = plantType;
        
    }

    public void StopPlanting()
    {
        mode = 0;
        GetComponent<Button>().interactable = false;
        choosingBorder.SetActive(false);
        potCollider.gameObject.SetActive(false);
    }

    public void StopFertilizing()
    {
        mode = 0;
        GetComponent<Button>().interactable = false;
        choosingBorder.SetActive(false);
        potCollider.gameObject.SetActive(false);
    }

    public void SetPlant(Plant plant)
    {
        potImage.sprite = usedPotSprite;
        currentPlant = plant;
    }

    public void RemovePlant()
    {
        potImage.sprite = emptyPotSprite;
        currentPlant = null;
    }

    public void ChoosePot()
    {
        switch (mode)
        {
            case 1: //PLANTING
                plantsManager.PlantSeedInPot(choosingPlant, this);
                GameObject.Find("UIManager").GetComponent<UIManager>().PlaySoundSeed();
                free = false;
                break;
            case 2: //FERTILIZING
                plantsManager.FertilizePlant();
                currentPlant.ApplyFertilizer(currentFertilizer);
                fertilizerManager.RemoveFertilizer(currentFertilizer);
                break;
        }
        
    }

    public void Fertilizing(Fertilizer fertilizer)
    {
        mode = 2;
        switch (fertilizer.type)
        {
            case 0: //STAT fertilizer
                if (currentPlant == null) return;
                //Condiciones para que no se pueda aplicar el fertilizante

                GetComponent<Button>().interactable = true;
                choosingBorder.SetActive(true);
                potCollider.gameObject.SetActive(true);
                currentFertilizer = fertilizer;
                break;

            case 1: //SKILL fertilizer
                if (currentPlant == null) return;
                if (currentPlant.skillsList.Count >= 3) return;
                if (currentPlant.skillsList.Contains(fertilizer.skill)) return;

                GetComponent<Button>().interactable = true;
                choosingBorder.SetActive(true);
                potCollider.gameObject.SetActive(true);
                currentFertilizer = fertilizer;
                break;

        }
    }
}
