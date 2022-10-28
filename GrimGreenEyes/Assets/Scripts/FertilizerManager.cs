using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FertilizerManager : MonoBehaviour
{
    public Dictionary<Fertilizer, int> storedFertilizers = new Dictionary<Fertilizer, int>(); //Almacena los fertilizantes y su cantidad
    private List<FertilizerButton> buttonsList = new List<FertilizerButton>(); //Almacena los SLOTS (objetos con botones) del inventario
    [SerializeField] private GameObject fertilizersPanel, fertilizerButtonPrefab, contentGameObject;
    [SerializeField] private TextMeshProUGUI fertilizerName, fertilizerDescription, powerupName, powerupDescription;
    [SerializeField] private GameObject powerupPanel, useButton;
    private Fertilizer selectedFertilizer;
    [SerializeField] private PlantsManager plantsManager;

    private void Start()
    {
        UpdateFertilizers();
    }

    public void OpenFertilizersList()
    {
        fertilizersPanel.SetActive(true);
        UpdateFertilizers();
    }

    public void CloseFertilizersList()
    {
        fertilizersPanel.SetActive(false);
    }

    public void AddFertilizer(Fertilizer fertilizer) //Añade un fertilizante al diccionario (inventario). Si ese fertilizante ya existía, aumenta su cantidad en 1 unidad.
    {
        if (storedFertilizers.ContainsKey(fertilizer))
        {
            storedFertilizers[fertilizer] = storedFertilizers[fertilizer] + 1;
        }
        else
        {
            storedFertilizers.Add(fertilizer, 1);
        }
    }

    public void RemoveFertilizer(Fertilizer fertilizer) //Reduce en 1 unidad la cantidad del fertilizante indicado. Si la cantidad llega a 0, el fertilizante es borrado de la lista.
    {
        if (!storedFertilizers.ContainsKey(fertilizer))
        {
            Debug.Log("THE ITEM " + fertilizer.name + " DOES NOT EXIST IN THE INVENTORY");
            return;
        }

        storedFertilizers[fertilizer] = storedFertilizers[fertilizer] - 1;
        if (storedFertilizers[fertilizer] <= 0)
        {
            storedFertilizers.Remove(fertilizer);
        }
    }

    public void UpdateFertilizers()
    {
        foreach (FertilizerButton button in buttonsList)
        {
            Destroy(button.gameObject);
        }
        buttonsList.Clear();

        foreach (KeyValuePair<Fertilizer, int> entry in storedFertilizers)
        {
            GameObject buttonAux = Instantiate(fertilizerButtonPrefab);
            buttonAux.transform.SetParent(contentGameObject.transform);
            buttonAux.transform.localScale = new Vector3(1, 1, 1);
            buttonAux.GetComponent<FertilizerButton>().Initialize(entry.Key, entry.Value);
            buttonsList.Add(buttonAux.GetComponent<FertilizerButton>());
        }
        ResetDescription();
    }

    public void ResetDescription()
    {
        powerupPanel.SetActive(false);
        useButton.SetActive(false);
        fertilizerName.text = Strings.FERTILIZERS_TITLE;
        fertilizerDescription.text = Strings.FERTILIZERS_INTRO;
    }

    public void UpdateDescription(Fertilizer fertilizer)
    {
        powerupPanel.SetActive(true);
        useButton.SetActive(true);
        fertilizerName.text = fertilizer.name;
        selectedFertilizer = fertilizer;

        switch (fertilizer.type)
        {
            case 0: //STAT fertilizer
                fertilizerDescription.text = Strings.STAT_FERT_DESC + fertilizer.stat.name + ".";
                powerupName.text = fertilizer.stat.name;
                powerupDescription.text = fertilizer.stat.description;
                break;
            case 1: //SKILL fertilizer
                fertilizerDescription.text = Strings.SKILL_FERT_DESC + fertilizer.skill.name + ".";
                powerupName.text = fertilizer.skill.name;
                powerupDescription.text = fertilizer.skill.description;
                break;
        }
    }

    public void ClickUseFertilizer()
    {
        CloseFertilizersList();
        plantsManager.UseFertilizer(selectedFertilizer);
    }

    #region DEBUG

    public Fertilizer fert1, fert2, fert3;

    public void AddFertilizers()
    {
        AddFertilizer(fert1);
        AddFertilizer(fert2);
        AddFertilizer(fert3);
    }

    #endregion
}
