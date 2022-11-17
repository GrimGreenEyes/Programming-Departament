using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiomController : MonoBehaviour
{
    // Start is called before the first frame update

    public int numberBiom;
    public int biom;

    public GlobalVar glovalBar;

    public List<GameObject> nodes;

    public Material[] materials;

    public Sprite[] sprites;

    public Sprite[] spritesFinales;

    public Sprite[] artPrfs;

    public GameObject artGameObject;


    void Start()
    {
        glovalBar = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>();

       // artGameObject = GameObject.Find("Arte");
        

                //biom = any.biomToInt(glovalBar.biomas[numberBiom]);

                int children = transform.childCount;
        for (int i = 0; i < children - 1; ++i)
        {
           // print("For loop: " + transform.GetChild(i));
            nodes.Add(transform.GetChild(i).gameObject);
        }

        //nodes = this.GetComponentsInChildren<GameObject>();
        //  setPaths();

        glovalBar.biomsList.Add(this.gameObject);
        EnumMapOptions.mapBiom biomtext = glovalBar.biomas[numberBiom];
        switch (biomtext)
        {
            case EnumMapOptions.mapBiom.desierto:
                biom = 0;
                break;
            case EnumMapOptions.mapBiom.llanura:
                biom = 1;
                break;
            case EnumMapOptions.mapBiom.nieve:
                biom = 2;
                break;
            case EnumMapOptions.mapBiom.selva:
                biom = 3;
                break;
        }
        setArt();

        setPaths();

    }

    public void setPaths()
    {
        for(int i = 0; i<nodes.Count; i++)
        {
            // materials[numberBiom] = nodes[i].GetComponent<PathOptions>().line.material;
            nodes[i].GetComponent<PathOptions>().line.material = materials[biom];
           if(!nodes[i].GetComponent<PathOptions>().isLast)
                nodes[i].GetComponent<Button>().image.sprite = sprites[biom];
            else
                nodes[i].GetComponent<Button>().image.sprite = spritesFinales[biom];
          
        }
    }

    public void setArt()
    {

        if(biom == 3)
        {
            Debug.Log("nieve");
        }
        foreach (Transform child in artGameObject.transform)
        {
            int randPick = biom;
            if(biom == 0)
            {
                int randBiomDes = Random.Range(0, 2);
                if (randBiomDes == 0)
                    randPick = 5;
                else if (randBiomDes == 1)
                    randPick = biom;
            }
            if (biom == 1)
            {
                int randBiomDes = Random.Range(0, 2);
                if (randBiomDes == 0)
                    randPick = 4;
                else if (randBiomDes == 1)
                    randPick = biom;
            }

            child.GetComponent<Image>().sprite = artPrfs[randPick];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
