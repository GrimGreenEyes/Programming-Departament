using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string displayText;
    private FollowMouse itemNameDisplay;
    private UIManager uiManager;

    private void Update()
    {
        if(uiManager == null)
        {
            uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        }

        if (itemNameDisplay == null)
        {
            itemNameDisplay = uiManager.followMouse;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemNameDisplay.ShowText(displayText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemNameDisplay.HideText();
    }

    public void OnDisable()
    {
        try
        {
            if (uiManager == null)
            {
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            }
            if (itemNameDisplay == null)
            {
                itemNameDisplay = uiManager.followMouse;
            }
            itemNameDisplay.HideText();
        }
        catch
        {

        }
        
    }
}
