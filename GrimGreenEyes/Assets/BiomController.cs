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

    void Start()
    {
        glovalBar = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>();

        

        //biom = any.biomToInt(glovalBar.biomas[numberBiom]);

        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
           // print("For loop: " + transform.GetChild(i));
            nodes.Add(transform.GetChild(i).gameObject);
        }

        //nodes = this.GetComponentsInChildren<GameObject>();
        //  setPaths();

        glovalBar.biomsList.Add(this.gameObject);
        Debug.Log(glovalBar.biomas[numberBiom]);
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
        setPaths();

    }

    public void setPaths()
    {
        for(int i = 0; i<nodes.Count; i++)
        {
            // materials[numberBiom] = nodes[i].GetComponent<PathOptions>().line.material;
            nodes[i].GetComponent<PathOptions>().line.material = materials[biom];
            nodes[i].GetComponent<Button>().image.sprite = sprites[biom];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
