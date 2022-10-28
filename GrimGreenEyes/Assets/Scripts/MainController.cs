using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName);
        if (string.Equals(sceneName, "MapScene"))
        {
            Debug.Log("IGUAL SCENEMAE");
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.returnMap);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);

        }
        else if(GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().isMapUp == true)
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(false);
        }

        if(string.Equals(sceneName, "MainScene"))
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchLoose);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(false);
        }
    }

    public void loadScreenFromBattle(bool won)
    {
        if (won)
        {
            SceneManager.LoadScene("MapScene");
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchWon);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);
        }
        else
        {
            loadScreen("MainScene");
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    /*
     * 
     *   private static IEnumerator LoadLevel (string sceneName){
         var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
         while (!asyncLoadLevel.isDone){
             Debug.Log("Loading the Scene"); 
             yield return null;
         }

         LoadScene?.Invoke(newSceneName);
     }
     * */
}
