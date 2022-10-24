using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject actualNode;
    public GameObject player;
    public GameObject camera;
    public float camSpeed;

    public Vector3 camFuturePos;


    //public Vector3 pathPos;

    public GameObject[] mapPrefabs;
    //public Vector3[] mapPos;

    public Vector3 mapPos = new Vector3(0,0,0);

    private int level;

    public GameObject glovalVar; 

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
            }

            glovalVar.GetComponent<GlobalVar>().mapGenerated.transform.parent = glovalVar.transform;
            glovalVar.GetComponent<GlobalVar>().created = 1;
        }
        else
        {
            //Do smth
        }


    }

    // Update is called once per frame
    void Update()
    {
        camFuturePos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        if(Vector3.Distance(camera.transform.position, player.transform.position) < 5){
            camera.transform.position = Vector3.Lerp(camera.transform.position, camFuturePos, Time.deltaTime * camSpeed);
        }
        else
            camera.transform.position = Vector3.Lerp(camera.transform.position, camFuturePos, Time.deltaTime * camSpeed*5);

    }

    public void moveToNode(GameObject node)
    {
        Debug.Log("click");
        // if (actualNode)
        //  GameObject isInList = actualNode.GetComponent<PathOptions>().myArray
        //Debug.Log("");
        //if(actualNode) // si el bot�n (nodo) est� en la lista del primero y est� activo, el nodo actual es el siguiente
        //
        actualNode = glovalVar.GetComponent<GlobalVar>().actualNode;
            if (actualNode.GetComponent<PathOptions>().getGameobjects(node))
            {
                Vector3 mediumPos = new Vector3(((actualNode.transform.position.x + node.transform.position.x)/2), ((actualNode.transform.position.y + node.transform.position.y)/2), -1.0f);
                player.transform.position = mediumPos;
                actualNode = node;
                glovalVar.GetComponent<GlobalVar>().actualNode = node;
            Debug.Log("cambia correcto");
                //player.transform.position = new Vector3(actualNode.transform.position.x, actualNode.transform.position.y , -1);

                camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position, Time.deltaTime * camSpeed);
                
                StartCoroutine(cameraMovement(node));

            }
            else
                Debug.Log(node.name + "not in list");
    }

    public void moveToNodeEmpty()
    {
        moveToNode(glovalVar.GetComponent<GlobalVar>().actualNode.GetComponent<PathOptions>().myArray[0].node);
    }

    IEnumerator cameraMovement(GameObject node){
        //Suponiendo que gana la partida

            yield return new WaitForSeconds(1);
            Debug.Log("Finished Coroutine::: PLAYER WON ");
        player.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, -1);
        camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position, Time.deltaTime * camSpeed);



    }
}
