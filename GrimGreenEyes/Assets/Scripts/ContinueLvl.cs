using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueLvl : MonoBehaviour
{
    
    void Start()
    {
        Button button = GetComponent<Button>();
        TextMeshProUGUI textMeshProUGUI = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().created == 0)
        {
            button.interactable = false;
            textMeshProUGUI.faceColor = new Color32(50, 50, 50, 100);
        }
        else
        {
            button.interactable = true;
            textMeshProUGUI.faceColor = new Color32(50, 50, 50, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
