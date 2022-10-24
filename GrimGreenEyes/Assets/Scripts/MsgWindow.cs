using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MsgWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI msgText;
    [SerializeField] private Button cancelButton;
    [SerializeField] private PlantsManager plantsManager;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowPlantingMsg(PlantType plant)
    {
        gameObject.SetActive(true);
        ResetWindow();
        ShowMsg("¿Dónde quieres plantar " + plant.name + "?");
        cancelButton.gameObject.SetActive(true);
        cancelButton.onClick.AddListener(ClickCancelPlanting);
    }

    public void ShowFertilizingMsg(Fertilizer fertilizer)
    {
        gameObject.SetActive(true);
        ResetWindow();
        ShowMsg("¿Dónde quieres utilizar " + fertilizer.name + "?");
        cancelButton.gameObject.SetActive(true);
        cancelButton.onClick.AddListener(ClickCancelFertilizing);
    }

    public void ClickCancelPlanting()
    {
        plantsManager.CancelPlanting();
        gameObject.SetActive(false);
    }

    public void ClickCancelFertilizing()
    {
        plantsManager.CancelFertilizing();
        gameObject.SetActive(false);
    }

    private void ResetWindow()
    {
        cancelButton.gameObject.SetActive(false);
        cancelButton.onClick.RemoveAllListeners();
    }

    private void ShowMsg(string msg)
    {
        msgText.text = msg;
    }
}
