using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{

    public bool matchHasWon;
    public bool movingCam;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        matchHasWon = false;

        if (!GameObject.Find("GlobalAttributes").gameObject.transform.Find("Fade"))
        {
          GameObject childFade = Instantiate(GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().fadePrefab);
            childFade.transform.SetParent(GameObject.Find("GlobalAttributes").transform);
            childFade.transform.SetAsFirstSibling();


        }
        Debug.Log(GameObject.Find("GlobalAttributes").gameObject.transform.Find("Fade").name);

        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScreen(string sceneName)
    {
        if (sceneName == "LoadMapScene")
        {
            SceneManager.LoadScene("MapScene");
        }
        else
            SceneManager.LoadScene(sceneName);

      // GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().doShowLoad();
        Debug.Log(sceneName);
        Debug.Log(matchHasWon);

        if (string.Equals(sceneName, "OptionsScene") || string.Equals(sceneName, "CreditsScene")){
            audioManager.clickAndChangeMusic("menu");

        }


            if (string.Equals(sceneName, "MapScene"))
        {
            audioManager.clickAndChangeMusic("mapa");
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().created == 1)
            {
                movingCam = true;
                GameObject helperNode = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().actualNode;
                GameObject.Find("Main Camera").transform.position = new Vector3(helperNode.transform.position.x, helperNode.transform.position.y, GameObject.Find("Main Camera").transform.position.z);
                movingCam = false;

                if (matchHasWon == false)
                {
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().doShowLoad();

                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.returnMap);
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);
                }
                else
                {
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.matchWon);
                    matchHasWon = false;
                    GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().mapGenerated.SetActive(true);

                   
                }

            }
            else
                GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().MapSceneUp(EnumMapOptions.mapOptions.returnMap);


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
            if (audioManager == null)
                audioManager = GameObject.Find("GlobalAttributes").GetComponent<AudioManager>();
            Debug.Log(audioManager.name);
            // audioManager.changeMusic("menu");
            audioManager.clickAndChangeMusic("menu");


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
            // audioManager.changeMusic("mapa");
            audioManager.clickAndChangeMusic("mapa");


        }
        if (string.Equals(sceneName, "CombatScene"))
        {
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().doShowLoad();
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomaActual == EnumMapOptions.mapBiom.desierto)
                audioManager.changeMusic("desierto");
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomaActual == EnumMapOptions.mapBiom.nieve)
                audioManager.changeMusic("nieve");
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomaActual == EnumMapOptions.mapBiom.llanura)
                audioManager.changeMusic("llanura1");
            if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomaActual == EnumMapOptions.mapBiom.selva)
                audioManager.changeMusic("jungla1");

        }
        if (string.Equals(sceneName, "ResourcesScene"))
        {
            audioManager.changeMusic("carro");
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().doShowLoad();

        }


    }

    public void loadScreenFromBattle(bool won)
    {
        if (won)
        {
            Debug.Log("LOAD SCREEN FROM BATTLE");
            Debug.Log(matchHasWon);
            
            matchHasWon = true;
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().nextNode();
            Debug.Log(matchHasWon);
            SceneManager.LoadScene("ResourcesScene");
            audioManager.changeMusic("carro");

        }
        else
        {
            loadScreen("MainScene");
            matchHasWon=false;
            GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().delete();
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
