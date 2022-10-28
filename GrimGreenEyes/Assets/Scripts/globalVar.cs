using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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




    public static GlobalVar VarInstance;
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
        biomaActual = biomas[0];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapSceneUp(EnumMapOptions.mapOptions advance)
    {
        StartCoroutine(waitForMapSceneLoad(advance));
        /*GameObject player = GameObject.Find("PLAYER");
        Debug.Log("SCNE UP");
        player.transform.position = actualNode.transform.position;*/
    }


    public IEnumerator waitForMapSceneLoad(EnumMapOptions.mapOptions advance)
    {
        Debug.Log("**********");
        Debug.Log(advance);
        yield return new WaitForSeconds(0.5f);
        GameObject player = GameObject.Find("PLAYER");
        Debug.Log("SCNE UP");
        isMapUp = true;

        if (advance == EnumMapOptions.mapOptions.matchWon)
        {
            GameObject.Find("NavigationController").GetComponent<Navigation>().matchWon(actualNode.GetComponent<PathOptions>().myArray[0].node);
        }

        else if(advance == EnumMapOptions.mapOptions.matchLoose)
        {
            Debug.Log("DELETING!!!");
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            created = 0;
            GameObject.Find("Canvas").GetComponent<MainController>().loadScreen("MainScene");

           // BALDO DESTRUYE AQU� LAS PLANTAS Y LO QUE TENGAS QUE CARGARTE
        }

        else if(advance == EnumMapOptions.mapOptions.returnMap)
        {
            player.transform.position = actualNode.transform.position;
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
