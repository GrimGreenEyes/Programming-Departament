using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantDetailsDisplay : MonoBehaviour
{
    private Plant currentPlant;
    [SerializeField] private TextMeshProUGUI plantName, plantAction, plantSkills;
    [SerializeField] private TextMeshProUGUI health, attack, agility, movement, deffense, heatRes, coldRes;
    [SerializeField] private StatRes statMaxHealth, statAttack, statAgility, statMovement, statDeffense, statHeatRes, statColdRes;
    [SerializeField] private Image plantImage;
    [SerializeField] private GameObject skillDisplay, skillsParent;
    private List<GameObject> skillsList = new List<GameObject>();
    private UIManager uiManager;
    private FollowMouse elementNameDisplay;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        elementNameDisplay = uiManager.followMouse;
        ClosePlantDetailsDisplay();
    }

    public void LoadPlantDetails(Plant plant)
    {
        currentPlant = plant;
        //UpdateDetailsDisplay();

        OpenPlantDetailsDisplay();
    }

    public void UpdateDetailsDisplay()
    {
        plantName.text = currentPlant.plantType.name;
        plantAction.text = Strings.ACTION + currentPlant.plantType.attack;
        plantSkills.text = Strings.SKILLS + currentPlant.skillsList.Count.ToString() + " / 3";

        health.text = currentPlant.healthPoints + " / " + currentPlant.GetMaxHealth();
        attack.text = currentPlant.statsDictionary[statAttack].ToString();
        agility.text = currentPlant.statsDictionary[statAgility].ToString();
        movement.text = currentPlant.statsDictionary[statMovement].ToString();
        deffense.text = currentPlant.statsDictionary[statDeffense].ToString();
        heatRes.text = currentPlant.statsDictionary[statHeatRes].ToString();
        coldRes.text = currentPlant.statsDictionary[statColdRes].ToString();
        //

        health.transform.parent.GetComponent<HoverElement>().displayText = statMaxHealth.description;
        attack.transform.parent.GetComponent<HoverElement>().displayText = statAttack.description;
        agility.transform.parent.GetComponent<HoverElement>().displayText = statAgility.description;
        movement.transform.parent.GetComponent<HoverElement>().displayText = statMovement.description;
        deffense.transform.parent.GetComponent<HoverElement>().displayText = statDeffense.description;
        heatRes.transform.parent.GetComponent<HoverElement>().displayText = statHeatRes.description;
        coldRes.transform.parent.GetComponent<HoverElement>().displayText = statColdRes.description;

        //
        plantImage.sprite = currentPlant.plantType.spriteSheet[currentPlant.plantState];

        foreach(GameObject display in skillsList)
        {
            GameObject.Destroy(display);
        }

        skillsList.Clear();

        foreach(SkillRes skill in currentPlant.skillsList)
        {
            GameObject skillDis = Instantiate(skillDisplay);
            skillDis.transform.SetParent(skillsParent.transform);
            skillDis.transform.position = new Vector3(0, 0, 0);
            skillDis.transform.localScale = new Vector3(1, 1, 1);
            skillDis.GetComponent<SkillDisplay>().DisplaySkill(skill.name);
            skillsList.Add(skillDis);
        }
    }

    public void OpenPlantDetailsDisplay()
    {
        gameObject.SetActive(true);
        UpdateDetailsDisplay();
        GameObject.Find("UIManager").GetComponent<UIManager>().HideExitButton();
    }

    public void ClosePlantDetailsDisplay()
    {
        gameObject.SetActive(false);
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowExitButton();
    }
}
