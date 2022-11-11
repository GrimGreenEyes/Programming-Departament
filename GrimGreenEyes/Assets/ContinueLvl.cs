using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueLvl : MonoBehaviour
{
    // Start is called before the first frame update
    public int lastLevel;
    public TextMeshProUGUI loadText;
    void Start()
    {
        loadText = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("level"))
        {
            lastLevel = PlayerPrefs.GetInt("level");
        }
        else
            lastLevel = 0;
       loadText.text = "Continue Level: " + lastLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
