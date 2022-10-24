using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodesPicker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] nodesToPick;
    public Sprite[] mapsToPick1;
    //	[SerializeField] public SubClass[] mapsToPick;

    //public Sprite[] mapsElements;
    public Sprite[] mapsToPick2;
    public Sprite[] mapsToPick3;



    private int rand;
    private int randTypes;
    //public GameObject firstChild;
    public bool isFirstBiom;

    public int mapsTypes;

    public int mapArrays;

    public int[] mapBioms;  //0 = desierto, 1 = llanura, 2 = nieve, 3 = selva

    private void Awake()
    {
        Object.DontDestroyOnLoad(this);
    }

    void Start()
    {
        rand = Random.Range(0, nodesToPick.Length);
        var nuevosNodos = Instantiate(nodesToPick[rand]);
        nuevosNodos.transform.parent = gameObject.transform;
        //randTypes = Random.Range(0, mapsTypes);
        //randTypes = Random.Range(0, );
        mapBioms = new int[mapArrays];


        if (mapArrays == 1)
        {
            randTypes = Random.Range(0, mapsToPick1.Length);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick1[randTypes];
            mapBioms[0] = randTypes % 4;
        }
        else if(mapArrays == 2)
        {
            int rand1 = Random.Range(0, mapsToPick1.Length);
            int rand2 = Random.Range(0, mapsToPick2.Length);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick1[rand1];
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = mapsToPick2[rand2];
            mapBioms[0] = rand1 % 4;
            mapBioms[1] = rand2 % 4;
        }

        GameObject.Find("GlobalAttributes").GetComponent<globalVar>().biomas = mapBioms;


        /*if (randTypes == 0)
        {
            var randMaps = Random.Range(0, mapsToPick1.Length);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick1[randMaps];

            if (mapsToPick1[randMaps].name == "Desierto")
            {

            }
            
        }
        else if (randTypes == 1)
        {
            var randMaps = Random.Range(0, mapsToPick2.Length);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick2[randMaps];

            if (mapsToPick2[randMaps].name == "Desierto")
            {

            }
        }

        else if (randTypes == 2)
        {
            var randMaps = Random.Range(0, mapsToPick3.Length);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick3[randMaps];

            if (mapsToPick3[randMaps].name == "Desierto")
            {

            }
        }

        //var randMaps = Random.Range(0, mapsToPick1.Length); SIEMPRE HAY EL MISMO NUMERO DE BIOMAS, 
        //BIOMAS EN MISMO ORDEN Y SE PUEDE COGER EL INDICE Y SABER QUE BIOMA ES

        */


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
