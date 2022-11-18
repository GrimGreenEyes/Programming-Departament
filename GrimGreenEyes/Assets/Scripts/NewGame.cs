using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NewGame : MonoBehaviour
{
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
        lastLevel += 1;
        loadText.text = "Empezar nuevo nivel : " + lastLevel;
    }

}
