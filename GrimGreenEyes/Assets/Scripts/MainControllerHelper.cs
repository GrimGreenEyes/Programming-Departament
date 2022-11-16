using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControllerHelper : MonoBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
    }
    
    public void clickWin()
    {
        GameObject.Find("GlobalAttributes").GetComponent<MainController>().loadScreenFromBattle(true);
    }

    public void loadScreen(string scena)
    {
        GameObject.Find("GlobalAttributes").GetComponent<MainController>().loadScreen(scena);
    }
    public void exitGame()
    {
        GameObject.Find("GlobalAttributes").GetComponent<MainController>().exitGame();
    }

    public void changeVolume(float sliderValue)
    {
        GameObject.Find("GlobalAttributes").GetComponent<AudioSource>().volume = sliderValue;
    }

    public void resetLevels()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 0);
            PlayerPrefs.Save();

            Debug.Log(PlayerPrefs.GetInt("level"));
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
