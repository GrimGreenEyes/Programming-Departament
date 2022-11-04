using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Button> sceneElements = new List<Button>();
    public PlantDetailsDisplay plantDetailsDisplay;
    public GameObject warningMsgPrefab;
    private GameObject currentWarning = null;

    private void Start()
    {

    }

    public void DesactivateAllElements()
    {
        foreach(Button button in sceneElements)
        {
            button.interactable = false;
        }
    }

    public void ActivateAllElements()
    {
        foreach (Button button in sceneElements)
        {
            button.interactable = true;
        }
    }

    public void AddButton(Button button)
    {
        sceneElements.Add(button);
    }

    public void ShowWarning(string msg, float time)
    {
        WarningMsg warning = Instantiate(warningMsgPrefab).GetComponent<WarningMsg>();
        Destroy(currentWarning);
        currentWarning = warning.gameObject;
        warning.DisplayMsg(msg, time);
    }
}
