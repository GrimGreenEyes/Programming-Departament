using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVar : MonoBehaviour
{
    // Start is called before the first frame update

    public int level;
    public int created;

    public GameObject mapGenerated;

    public GameObject actualNode;

    public bool isMapUp;

    public EnumMapOptions.mapBiom [] biomas;

    public EnumMapOptions.mapBiom biomaActual;//0 = desierto, 1 = llanura, 2 = nieve, 3 = selva

    public bool isLastNode;


    public List<Transform> biomCont;


    public static GlobalVar VarInstance;


    private EnumMapOptions any;

    public int indBioma;

    public List<GameObject> biomsList;
    void Awake()
    {
        isMapUp = true;
        DontDestroyOnLoad(this);

        if (VarInstance == null)
        {
            VarInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
   /* private void Awake()
    {
        if (GameObject.Find("GlobalAttributes"))
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        //created = 0;
    }*/
    void Start()
    {
       // biomaActual = biomas[0];
       // checkBioms("CanvasNodes");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapSceneUp(EnumMapOptions.mapOptions advance)
    {
        StartCoroutine(waitForMapSceneLoad(advance));
        /*GameObject player = GameObject.Find("PLAYER");
        player.transform.position = actualNode.transform.position;*/
    }

    public void checkBioms(string name)
    {
       

        foreach (GameObject bb in biomsList)
        {
            bb.GetComponent<BiomController>().biom = any.biomToInt(biomas[bb.GetComponent<BiomController>().numberBiom]);
            bb.GetComponent<BiomController>().setPaths();
        }
        
            
        
    }


    public IEnumerator waitForMapSceneLoad(EnumMapOptions.mapOptions advance)
    {
        if(advance == EnumMapOptions.mapOptions.loadGame)
        {
            level = PlayerPrefs.GetInt("level");
        }

        while(SceneManager.GetActiveScene().name != "MapScene")
        {
            yield return null;
        }

        if (level >= PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("level", level);
            PlayerPrefs.Save();
        }
        


        GameObject player = GameObject.Find("PLAYER");
        isMapUp = true;

        if (advance == EnumMapOptions.mapOptions.matchWon)
        {
            GameObject.Find("NavigationController").GetComponent<Navigation>().matchWon(actualNode.GetComponent<PathOptions>().myArray[0].node);
        }

        else if(advance == EnumMapOptions.mapOptions.matchLoose)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            created = 0;
            //ESTA LÍNEA COMENTADA DABA ERROR. SI LA DESCOMENTO FUNCIONA BIEN, AL MENOS DESDE EL BOTÓN DE TERMINAR COMBATE
            //GameObject.Find("Canvas").GetComponent<MainController>().loadScreen("MainScene");

            //Destrucción de todos los recursos
            TeamInfo teamInfo = GetComponent<TeamInfo>();
            teamInfo.plantsList = new List<PlantInfo>();
            teamInfo.fertilizersDictionary = new Dictionary<Fertilizer, int>();
            teamInfo.itemsDictionary = new Dictionary<Item, int>();
            teamInfo.waterAmount = 0;

            teamInfo.AddInitialPlant();
        }

        else if(advance == EnumMapOptions.mapOptions.returnMap || advance == EnumMapOptions.mapOptions.loadGame)
        {
            player.transform.position = actualNode.transform.position;
            Vector3 vectorToTarget =actualNode.GetComponent<PathOptions>().myArray[0].node.GetComponent<PathOptions>().line.GetPosition(1) - actualNode.GetComponent<PathOptions>().myArray[0].node.GetComponent<PathOptions>().line.GetPosition(0);
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            player.transform.rotation = q;
        }
    }

    public void loadNewScene()
    {
        level++;
       
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        created = 0;
        biomaActual = biomas[0];
    }

    public void matchTest()
    {
        MapSceneUp(EnumMapOptions.mapOptions.matchWon);
    }

    IEnumerator waitForMapSceneLoadNodeMove()
    {
        yield return new WaitForSeconds(0.5f);

    }



}
