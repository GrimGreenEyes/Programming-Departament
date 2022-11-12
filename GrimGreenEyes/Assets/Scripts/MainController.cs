using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{

    public bool matchHasWon;
    public bool movingCam;
    // Start is called before the first frame update
    void Start()
    {
        matchHasWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScreen(string sceneName)
    {
        if (sceneName == "LoadMapScene")
            SceneManager.LoadScene("MapScene");
        else
            SceneManager.LoadScene(sceneName);

        Debug.Log(sceneName);
        Debug.Log(matchHasWon);
       
        if (string.Equals(sceneName, "MapScene"))
        {
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().created == 1)
            {
                movingCam = true;
                GameObject helperNode = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().actualNode;
                GameObject.Find("Main Camera").transform.position = new Vector3(helperNode.transform.position.x, helperNode.transform.position.y, GameObject.Find("Main Camera").transform.position.z);
                movingCam = false;

                if (matchHasWon == false)
                {
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.returnMap);
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);
                }
                else
                {
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchWon);
                    matchHasWon = false;
                }
                
            }
            else
                GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.loadGame);


        }
        else if (GameObject.Find("GlobalAttributes")) {
         if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().isMapUp == true)
            {
                if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated != null)
                {
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(false);
                }
            }
        }

        if(string.Equals(sceneName, "MainScene"))
        {
          //  GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchLoose);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(false);
        }

        if (string.Equals(sceneName, "LoadMapScene"))
        {
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().created == 0)
            {
                GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.loadGame);
            }
            else
            {
                //GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchLoose);
                GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().delete();
               // StartCoroutine(wait());
              //  GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().created = 0;
                GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.loadGame);
                GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);
            }
        }
    }

    public void loadScreenFromBattle(bool won)
    {
        if (won)
        {/*
            SceneManager.LoadScene("MapScene");
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchWon);
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);
            */
            Debug.Log("LOAD SCREEN FROM BATTLE");
            Debug.Log(matchHasWon);
            matchHasWon = true;
            Debug.Log(matchHasWon);
            SceneManager.LoadScene("ResourcesScene");
        }
        else
        {
            loadScreen("MainScene");
        }
    }

    public void loadMapScreenFromRes()
    {

    }

    public void exitGame()
    {
        Application.Quit();
    }


    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
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
