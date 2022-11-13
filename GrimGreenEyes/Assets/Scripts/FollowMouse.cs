using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    private Canvas myCanvas;
    public TextMeshProUGUI itemNameText;

    void Start()
    {
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        HideText();
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(new Vector2(pos.x + 25, pos.y));
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void EnableObject()
    {
        gameObject.SetActive(true);
    }

    public void HideText()
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0);
        itemNameText.color = new Color(itemNameText.color.r, itemNameText.color.g, itemNameText.color.b, 0);
    }

    public void ShowText(string text)
    {
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1);
        itemNameText.color = new Color(itemNameText.color.r, itemNameText.color.g, itemNameText.color.b, 1);
        itemNameText.text = text;
        
    }
}
