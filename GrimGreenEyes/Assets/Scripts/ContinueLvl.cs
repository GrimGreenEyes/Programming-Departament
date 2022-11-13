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

        if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().created == 0)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
