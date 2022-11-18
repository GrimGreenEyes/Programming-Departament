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
    private Slider sliderBar;

    private Dictionary<StatRes, int> upgradeStatValue = new Dictionary<StatRes, int>();

    public int healthPoints;

    public List<GameObject> idles = new List<GameObject>();

    public string TempAttack;

    //SKILLS
    public List<SkillRes> skillsList = new List<SkillRes>();
    //

    [SerializeField] private TextMeshProUGUI plantStatusText;
    private string[] states = { "", "", "adult" };
    PlantDetailsDisplay detailsDisplay;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        detailsDisplay = uiManager.plantDetailsDisplay;
        uiManager.AddButton(GetComponent<Button>());
        sliderBar = GameObject.Find("HealthBar").GetComponent<Slider>();

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
            transform.localPosition = new Vector3(-6f, 42f, 0f);
            GetComponent<Image>().enabled = false;
            plantStatusText.text = healthPoints.ToString() + " / " + statsDictionary[statMaxHealth].ToString();
            sliderBar.value = (float)healthPoints / (float)statsDictionary[statMaxHealth];
            sliderBar.gameObject.SetActive(true);
            idles[plantType.idleIndex].SetActive(true);
            GetComponent<RectTransform>().sizeDelta = new Vector2(130, GetComponent<RectTransform>().sizeDelta.y);

        }
        else
        {
            foreach(GameObject item in idles)
            {
                item.SetActive(false);
            }
            plantStatusText.text = states[plantState];
            sliderBar.gameObject.SetActive(false);
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

        upgradeStatValue.Add(statMaxHealth, plantType.upgradeHP);
        upgradeStatValue.Add(statAttack, plantType.upgradeAttack);
        upgradeStatValue.Add(statMovement, plantType.upgradeMovement);
        upgradeStatValue.Add(statAgility, plantType.upgradeAgility);
        upgradeStatValue.Add(statDeffense, plantType.upgradeDeffense);
        upgradeStatValue.Add(statHeatRes, plantType.upgradeHeatDef);
        upgradeStatValue.Add(statColdRes, plantType.upgradeColdDef);

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
                statsDictionary[fertilizer.stat] += upgradeStatValue[fertilizer.stat];
                UpdatePlantVisuals();
                break;
            case 1: //SKILL fertilizer
                skillsList.Add(fertilizer.skill);
                break;
        }
    }
}
