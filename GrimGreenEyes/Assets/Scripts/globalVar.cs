using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalVar : MonoBehaviour
{
    // Start is called before the first frame update

    public int level;
    public int created;

    public GameObject mapGenerated;

    public GameObject actualNode;

    public bool isMapUp;



    private static globalVar VarInstance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapSceneUp()
    {
        StartCoroutine(waitForMapSceneLoad());
        /*GameObject player = GameObject.Find("PLAYER");
        Debug.Log("SCNE UP");
        player.transform.position = actualNode.transform.position;*/
    }

    public void MapSceneDown()
    {

    }

    IEnumerator waitForMapSceneLoad()
    {
        yield return new WaitForSeconds(1);
        GameObject player = GameObject.Find("PLAYER");
        Debug.Log("SCNE UP");
        isMapUp = true;
        player.transform.position = actualNode.transform.position;
    }




}
