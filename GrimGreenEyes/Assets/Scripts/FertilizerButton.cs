using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FertilizerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText, amountText;
    private Fertilizer listedFertilizer;
    [SerializeField] private FertilizerManager fertilizerManager;

    public void Initialize(Fertilizer fertilizer, int amount)
    {
        fertilizerManager = GameObject.Find("Fertilizers").GetComponent<FertilizerManager>();
        nameText.text = fertilizer.name;
        amountText.text = amount.ToString();
        listedFertilizer = fertilizer;
        GetComponent<Button>().onClick.AddListener(ClickShowFertilizerDescription);
    }

    public void ClickShowFertilizerDescription()
    {
        fertilizerManager.UpdateDescription(listedFertilizer);
    }
}
