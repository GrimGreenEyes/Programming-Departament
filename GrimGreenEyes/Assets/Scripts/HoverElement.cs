using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string displayText;
    private FollowMouse itemNameDisplay;
    private UIManager uiManager;

    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        itemNameDisplay = uiManager.followMouse;
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
        itemNameDisplay.HideText();
    }
}
