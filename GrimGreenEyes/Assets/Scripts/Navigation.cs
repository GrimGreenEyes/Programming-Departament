using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject actualNode;
    public GameObject player;
    public GameObject camera;
    public float camSpeed;

    public float playerSpeed;


    public Vector3 camFuturePos;


    //public Vector3 pathPos;

    public GameObject[] mapPrefabs;
    //public Vector3[] mapPos;

    public Vector3 mapPos = new Vector3(0,0,0);

    private int level;

    public GameObject glovalVar;

    public bool movingCam;

    void Start()
    {
        glovalVar = GameObject.Find("GlobalAttributes");

       // glovalVar = GlobalVar.VarInstance;
        level = glovalVar.GetComponent<GlobalVar>().level;
        if (glovalVar.GetComponent<GlobalVar>().created == 0)
        {
            switch (level)
            {
                case 0:
                    glovalVar.GetComponent<GlobalVar>().mapGenerated = Instantiate(mapPrefabs[0], mapPos, Quaternion.identity);
                    break;

                case 1:
                    glovalVar.GetComponent<GlobalVar>().mapGenerated = Instantiate(mapPrefabs[1], mapPos, Quaternion.identity);
                    //Instantiate(mapPrefabs[2], mapPos[2], Quaternion.identity);
                    break;

                case 2:
                    glovalVar.GetComponent<GlobalVar>().mapGenerated = Instantiate(mapPrefabs[2], mapPos, Quaternion.identity);
                    break;


                case 3:
                    /*Instantiate(mapPrefabs[3], mapPos[3], Quaternion.identity);
                    Instantiate(mapPrefabs[4], mapPos[4], Quaternion.identity);
                    Instantiate(mapPrefabs[5], mapPos[5], Quaternion.identity);
                    Instantiate(mapPrefabs[6], mapPos[6], Quaternion.identity);
                    */
                    glovalVar.GetComponent<GlobalVar>().mapGenerated = Instantiate(mapPrefabs[3], mapPos, Quaternion.identity);

                    break;
                case 4:
                    PlayerPrefs.SetInt("level", 2);
                    PlayerPrefs.Save();
                    glovalVar.GetComponent<MainController>().loadScreen("MainScene");
                    break;
            }

            glovalVar.GetComponent<GlobalVar>().mapGenerated.transform.parent = glovalVar.transform;
            glovalVar.GetComponent<GlobalVar>().created = 1;


            // actualNode = glovalVar.GetComponent<GlobalVar>().actualNode;
            //  player.transform.LookAt(actualNode.GetComponent<PathOptions>().myArray[0].node.transform);

            /* var lookPos = player.transform.position - actualNode.GetComponent<PathOptions>().myArray[0].node.transform.position;
             lookPos.z = 0;
             var rotation = Quaternion.LookRotation(lookPos);
             player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, 1);
             player.transform.LookAt()
            */
            // player.transform.rotation = Quaternion.LookRotation(Vector3.forward,  actualNode.GetComponent<PathOptions>().myArray[0].node.transform.rotation));

            Debug.Log("MAP GENERATION!!");
            glovalVar.GetComponent<GlobalVar>().numNode = 0;
        }
        else
        {
            actualNode = glovalVar.GetComponent<GlobalVar>().actualNode;
            //glovalVar.GetComponent<GlobalVar>().numNode = 0;

        }


    }

    // Update is called once per frame
    void Update()
    {
        camFuturePos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        if(!GameObject.Find("GlobalAttributes").GetComponent<MainController>().movingCam){
            camera.transform.position = Vector3.Lerp(camera.transform.position, camFuturePos, Time.deltaTime * camSpeed);
        }
        else
            camera.transform.position = new Vector3(actualNode.transform.position.x, actualNode.transform.position.y, camera.transform.position.z);


        Vector3 mediumPos = new Vector3(((actualNode.transform.position.x + glovalVar.GetComponent<GlobalVar>().actualNode.transform.position.x) / 2), ((actualNode.transform.position.y + glovalVar.GetComponent<GlobalVar>().actualNode.transform.position.y) / 2), -1.0f);
        player.transform.position = Vector3.Lerp(player.transform.position, mediumPos, Time.deltaTime * playerSpeed);

        //Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, mediumPos);
       // player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, Time.deltaTime * playerSpeed);
      //  player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(mediumPos), Time.deltaTime * playerSpeed);
    }

    public void moveToNode(GameObject node)
    {
       
        actualNode = glovalVar.GetComponent<GlobalVar>().actualNode;
            if (actualNode.GetComponent<PathOptions>().getGameobjects(node))
            {
                Vector3 mediumPos = new Vector3(((actualNode.transform.position.x + node.transform.position.x)/2), ((actualNode.transform.position.y + node.transform.position.y)/2), -1.0f);
                //player.transform.position = mediumPos;
                actualNode = node;

                camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position, Time.deltaTime * camSpeed);
                
                StartCoroutine(cameraMovement(node));

            }
            else
                Debug.Log(node.name + "not in list");
    }
    
    public void matchWonDef()
    {

        if (!glovalVar.GetComponent<GlobalVar>().isLastNode)
        {
            player.transform.position = new Vector3(actualNode.transform.position.x, actualNode.transform.position.y, -1);
            Vector3 playerPos = new Vector3(actualNode.transform.position.x, actualNode.transform.position.y, player.transform.position.z);
            Debug.Log("MATCH WON");

            Vector3 vectorToTarget = actualNode.GetComponent<PathOptions>().myArray[0].node.GetComponent<PathOptions>().line.GetPosition(1) - actualNode.GetComponent<PathOptions>().myArray[0].node.GetComponent<PathOptions>().line.GetPosition(0);
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject.Find("PLAYER").transform.rotation = q;
        }


        if (actualNode.GetComponent<PathOptions>().myArray[0].node.GetComponent<PathOptions>().isLast)
        {
            glovalVar.GetComponent<GlobalVar>().isLastNode = true;
        }
        else
            glovalVar.GetComponent<GlobalVar>().isLastNode = false;
    }
  
    public void matchLoose()
    {

    }

    public void moveToNodeEmpty()
    {
        moveToNode(glovalVar.GetComponent<GlobalVar>().actualNode.GetComponent<PathOptions>().myArray[0].node);
        StartCoroutine(glovalVar.GetComponent<GlobalVar>().actualNode.GetComponent<PathOptions>().myArray[0].node.GetComponent<PathOptions>().goMatch());
                //		StartCoroutine(goMatch());

    }

    IEnumerator cameraMovement(GameObject node){
        //Suponiendo que gana la partida

            yield return new WaitForSeconds(1);
            Debug.Log("Finished Coroutine::: PLAYER WON ");
        // player.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, -1);
        //matchWon(node);
        camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position, Time.deltaTime * camSpeed);



    }
}
