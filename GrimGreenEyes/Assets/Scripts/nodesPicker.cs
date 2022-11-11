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
    public Sprite[] mapsToPick4;




    private int rand;
    private int randTypes;
    //public GameObject firstChild;
    public bool isFirstBiom;

    public int mapsTypes;

    public int mapArrays;

    public int[] mapBioms;  //0 = desierto, 1 = llanura, 2 = nieve, 3 = selva

    public EnumMapOptions.mapBiom [] mapOpt;

    //biom which belongs
   // public EnumMapOptions.mapBiom thisBiom;

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
        mapOpt = new EnumMapOptions.mapBiom[mapArrays];


            randTypes = Random.Range(0, mapsToPick1.Length);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick1[randTypes];
            mapBioms[0] = randTypes % 4;
            switch (mapBioms[0])
            {
                case 0:
                    mapOpt[0] = EnumMapOptions.mapBiom.desierto;
                    break;
                case 1:
                    mapOpt[0] = EnumMapOptions.mapBiom.llanura;
                    break;
                case 2:
                    mapOpt[0] = EnumMapOptions.mapBiom.nieve;
                    break;
                case 3:
                    mapOpt[0] = EnumMapOptions.mapBiom.selva;
                    break;
            
        };
         if(mapArrays != 1)
        {
            //   int rand1 = Random.Range(0, mapsToPick1.Length);
            /* int rand2 = Random.Range(0, mapsToPick2.Length);
             //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapsToPick1[rand];
             transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = mapsToPick2[rand2];
             mapBioms[1] = rand2 % 4;
             Debug.Log(mapBioms[0]);
             Debug.Log(mapBioms[1]);
            */
            int rand2 = Random.Range(0, mapsToPick2.Length);
            Debug.Log(rand2%4);
            Debug.Log(mapBioms[0]);
            while(rand2%4 == mapBioms[0])
            {

                 rand2 = Random.Range(0, mapsToPick2.Length);
                Debug.Log(rand2 % 4);

            }
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = mapsToPick2[rand2];
            mapBioms[1] = rand2 % 4;


            switch (mapBioms[1])
            {
                case 0:
                    mapOpt[1] = EnumMapOptions.mapBiom.desierto;
                    break;
                case 1:
                    mapOpt[1] = EnumMapOptions.mapBiom.llanura;
                    break;
                case 2:
                    mapOpt[1] = EnumMapOptions.mapBiom.nieve;
                    break;
                case 3:
                    mapOpt[1] = EnumMapOptions.mapBiom.selva;
                    break;
            }
        }



        if (mapArrays == 3 || mapArrays == 4 )
        {
            int rand3 = Random.Range(0, mapsToPick3.Length);
            Debug.Log(mapBioms[0]);
            while (rand3 % 4 == mapBioms[0] || rand3 % 4 == mapBioms[1])
            {

                rand3 = Random.Range(0, mapsToPick3.Length);
                Debug.Log(rand3 % 4);

            }
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = mapsToPick3[rand3];
            mapBioms[2] = rand3 % 4;

            switch (mapBioms[2])
            {
                case 0:
                    mapOpt[2] = EnumMapOptions.mapBiom.desierto;
                    break;
                case 1:
                    mapOpt[2] = EnumMapOptions.mapBiom.llanura;
                    break;
                case 2:
                    mapOpt[2] = EnumMapOptions.mapBiom.nieve;
                    break;
                case 3:
                    mapOpt[2] = EnumMapOptions.mapBiom.selva;
                    break;
            }
        }

       

        if (mapArrays == 4)
        {
            int rand4 = Random.Range(0, mapsToPick4.Length);
            Debug.Log(mapBioms[0]);
            while (rand4 % 4 == mapBioms[0] || rand4 % 4 == mapBioms[1] || rand4 % 4 == mapBioms[2])
            {

                rand4 = Random.Range(0, mapsToPick4.Length);
                Debug.Log(rand4 % 4);

            }
            transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = mapsToPick3[rand4];
            mapBioms[3] = rand4 % 4;

            switch (mapBioms[3])
            {
                case 0:
                    mapOpt[3] = EnumMapOptions.mapBiom.desierto;
                    break;
                case 1:
                    mapOpt[3] = EnumMapOptions.mapBiom.llanura;
                    break;
                case 2:
                    mapOpt[3] = EnumMapOptions.mapBiom.nieve;
                    break;
                case 3:
                    mapOpt[3] = EnumMapOptions.mapBiom.selva;
                    break;
            }

        }




        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomas = mapOpt;
        GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomaActual = mapOpt[0];



        for (int i = 0; i< GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomas.Length; i++)
        {
          Debug.Log(GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().biomas[i]);
        }


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
