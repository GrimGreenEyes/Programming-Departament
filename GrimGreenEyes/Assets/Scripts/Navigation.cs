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

    private Vector3 camFuturePos;

    void Start()
    {
        
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
            if(actualNode.GetComponent<PathOptions>().getGameobjects(node))
            {
                actualNode = node;
                Debug.Log("cambia correcto");
                player.transform.position = new Vector3(actualNode.transform.position.x, actualNode.transform.position.y , -1);

                camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position, Time.deltaTime * camSpeed);
                //StartCoroutine(cameraMovement());
            }
            else
                Debug.Log("not in list");
    }

    IEnumerator cameraMovement(){

            yield return new WaitForSeconds(5);
            Debug.Log("Finished Coroutine ");


    }
}
