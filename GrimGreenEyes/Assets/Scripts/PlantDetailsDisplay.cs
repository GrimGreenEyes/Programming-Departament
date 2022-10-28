using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantDetailsDisplay : MonoBehaviour
{
    private Plant currentPlant;
    [SerializeField] private TextMeshProUGUI plantName, plantLevel, plantSkills;
    [SerializeField] private TextMeshProUGUI plantDamage, plantCurrentHp, plantMaxHp, plantMovement, plantAgility, plantInitiative;
    [SerializeField] private StatRes statMaxHealth, statAttack, statAgility, statMovement, statDeffense, statHeatRes, statColdRes;
    [SerializeField] private Image plantImage;

    private void Start()
    {
        ClosePlantDetailsDisplay();
    }

    public void LoadPlantDetails(Plant plant)
    {
        currentPlant = plant;
        UpdateDetailsDisplay();

        OpenPlantDetailsDisplay();
    }

    public void UpdateDetailsDisplay()
    {
        /*plantName.text = currentPlant.plantType.name;
        plantLevel.text = Strings.LEVEL + currentPlant.plantLevel.ToString();
        plantSkills.text = Strings.SKILLS;

        plantDamage.text = currentPlant.damage.ToString();
        plantCurrentHp.text = currentPlant.healthPoints.ToString();
        plantMaxHp.text = currentPlant.maxHP.ToString();
        plantMovement.text = currentPlant.movement.ToString();
        plantAgility.text = currentPlant.agility.ToString();
        plantInitiative.text = currentPlant.initiative.ToString();

        plantImage.sprite = currentPlant.plantType.spriteSheet[currentPlant.plantState];*/
    }

    public void OpenPlantDetailsDisplay()
    {
        gameObject.SetActive(true);
        UpdateDetailsDisplay();
    }

    public void ClosePlantDetailsDisplay()
    {
        gameObject.SetActive(false);
    }
}
