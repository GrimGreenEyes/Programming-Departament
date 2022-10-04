using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    private UIManager uiManager;

    //STATS
    public PlantType plantType;
    public int plantState = 0; //0 = baby; 1 = middle; 3 = adult
    public int healthPoints, maxHP;
    public int plantLevel = 1;
    public int damage, movement, agility, initiative;
    //

    [SerializeField] private TextMeshProUGUI plantStatusText;
    private string[] states = { "baby", "middle", "adult" };
    PlantDetailsDisplay detailsDisplay;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        detailsDisplay = uiManager.plantDetailsDisplay;
        uiManager.AddButton(GetComponent<Button>());

        UpdatePlantVisuals();
    }

    private void UpdatePlantVisuals()
    {
        GetComponent<Image>().sprite = plantType.spriteSheet[plantState];
        if(plantState >= 2)
        {
            plantStatusText.text = healthPoints.ToString() + " / " + maxHP.ToString();
        }
        else
        {
            plantStatusText.text = states[plantState];
        }
    }

    public void OpenPlantDetails()
    {
        detailsDisplay.LoadPlantDetails(this);
    }

    private void InitializeStats()
    {
        healthPoints = plantType.baseHP;
        maxHP = plantType.baseHP;
        damage = plantType.baseDamage;
        movement = plantType.baseMovement;
        agility = plantType.baseAgility;
        initiative = plantType.baseInitiative;
        //GetComponent<Image>().color = plantType.color;
    }

    public void ReceiveWater()
    {
        if(plantState <= 1)
        {
            plantState++;
        }
        else
        {
            healthPoints += 10;
        }

        UpdatePlantVisuals();
    }

    public void SetPlantType(PlantType type)
    {
        plantType = type;
        InitializeStats();
    }
}
