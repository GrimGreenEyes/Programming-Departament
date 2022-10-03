using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Button> sceneElements = new List<Button>();

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
}
