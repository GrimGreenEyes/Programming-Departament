using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControllerHelper : MonoBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
    }
    
    public void clickWin(bool win)
    {
        GameObject.Find("GlobalAttributes").GetComponent<MainController>().loadScreenFromBattle(win);
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
        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().firstTimeResources = true;
        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().firstTimeMap = true;
        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().firstTimeCombat = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
