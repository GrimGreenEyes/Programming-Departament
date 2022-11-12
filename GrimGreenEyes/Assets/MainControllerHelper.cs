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
    // Update is called once per frame
    void Update()
    {
        
    }
}
