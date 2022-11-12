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

    public Dictionary<StatRes, int> statsDictionary = new Dictionary<StatRes, int>();
    [SerializeField] private StatRes statMaxHealth, statAttack, statAgility, statMovement, statDeffense, statHeatRes, statColdRes;

    public int healthPoints;

    public string TempAttack;

    //SKILLS
    public List<SkillRes> skillsList = new List<SkillRes>();
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
        GetComponent<Image>().SetNativeSize();
        switch (plantState)
        {
            case 0:
                transform.localPosition = new Vector3(5f, 50f, 0f);
                break;
            case 1:
                transform.localPosition = new Vector3(-6f, 35f, 0f);
                break;
            default:
                break;
        }
        if (plantState >= 2)
        {
            plantStatusText.text = healthPoints.ToString() + " / " + statsDictionary[statMaxHealth].ToString();
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

        statsDictionary[statMaxHealth] = plantType.baseHP;
        statsDictionary[statAttack] = plantType.baseAttack;
        statsDictionary[statMovement] = plantType.baseMovement;
        statsDictionary[statAgility] = plantType.baseAgility;
        statsDictionary[statDeffense] = plantType.baseDeffense;
        statsDictionary[statHeatRes] = plantType.baseHeatDef;
        statsDictionary[statColdRes] = plantType.baseColdDef;

        TempAttack = plantType.attack;

        skillsList.Add(plantType.baseSkill);
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
            if (healthPoints >= GetMaxHealth()) healthPoints = GetMaxHealth();
        }

        UpdatePlantVisuals();
    }

    public void SetPlantType(PlantType type)
    {
        plantType = type;
        InitializeStats();
    }

    public int GetMaxHealth()
    {
        return statsDictionary[statMaxHealth];
    }

    public void ApplyFertilizer(Fertilizer fertilizer)
    {
        switch (fertilizer.type)
        {
            case 0: //STAT fertilizer
                //Pendiente aplicar algoritmos de subida de nivel
                statsDictionary[fertilizer.stat] ++;
                break;
            case 1: //SKILL fertilizer
                skillsList.Add(fertilizer.skill);
                break;
        }
    }
}
