using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodesPicker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] nodesToPick;
    public Sprite[] mapsToPick;

    private int rand;
    //public GameObject firstChild;
    public bool isFirstBiom;

    void Start()
    {
        rand = Random.Range(0, nodesToPick.Length);
        var nuevosNodos = Instantiate(nodesToPick[rand]);
        nuevosNodos.transform.parent = gameObject.transform;

        var randMaps = Random.Range(0, mapsToPick.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick[randMaps];
      /*  if (isFirstBiom)
        {
            foreach (Transform eachChild in transform)
            {
                if (eachChild.name == "CanvasNodes")
                {
                    eachChild.transform.GetChild(0).GetComponent<PathOptions>().firstItem = true;
                    //eachChild.GetComponent<PathOptions>().firstItem = true;
                }
            }
        }
      */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
