using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    private Canvas myCanvas;
    public TextMeshProUGUI itemNameText;
    public GameObject text;
    public int baseWidth = 500;
    public int margin = 5;
    private RectTransform thisTransform, canvasTransform;

    void Start()
    {
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        thisTransform = GetComponent<RectTransform>();
        canvasTransform = myCanvas.GetComponent<RectTransform>();
        HideText();
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(new Vector2(pos.x + 25, pos.y));

        if (thisTransform.anchoredPosition.x + thisTransform.sizeDelta.x >= canvasTransform.sizeDelta.x / 2)
        {
            //Debug.Log("OUT");
            thisTransform.pivot = new Vector2(1.1f, 0.5f);
        }
        else
        {
            thisTransform.pivot = new Vector2(0f, 0.5f);
        }


        /*if(text.GetComponent<RectTransform>().rect.width < baseWidth - (2 * margin))
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(text.GetComponent<RectTransform>().rect.width + (2 * margin), gameObject.GetComponent<RectTransform>().sizeDelta.y);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(baseWidth, gameObject.GetComponent<RectTransform>().sizeDelta.y);
        }*/
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
