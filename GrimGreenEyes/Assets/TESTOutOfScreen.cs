using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTOutOfScreen : MonoBehaviour
{
    public Canvas canvas;
    private RectTransform thisTransform, canvasTransform;

    private void Start()
    {
        thisTransform = GetComponent<RectTransform>();
        canvasTransform = canvas.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(thisTransform.anchoredPosition.x + thisTransform.sizeDelta.x >= canvasTransform.sizeDelta.x / 2)
        {
            Debug.Log("OUT");
        }
    }
}
