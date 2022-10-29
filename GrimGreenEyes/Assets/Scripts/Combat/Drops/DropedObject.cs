using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedObject : MonoBehaviour
{
    public new string name;
    private enum Seeds { VERA, LILY, SUNFLOWER, CACTUS, ROSE, FUNGUS, CORN, TUMBLEWEED}
    [SerializeField]private Seeds seed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return;
        }
        PickUpObject();
        Destroy(gameObject);
    }
    private void PickUpObject()
    {
        switch (seed)
        {
            case Seeds.VERA:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedAloeVera();
                break;
            case Seeds.LILY:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedLily();
                break;
            case Seeds.SUNFLOWER:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedSunflower();
                break;
            case Seeds.ROSE:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedRose();
                break;
            case Seeds.FUNGUS:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedCarnivorousFungus();
                break;
            case Seeds.CACTUS:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedCactus();
                break;
            case Seeds.CORN:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedCorn();
                break;
            case Seeds.TUMBLEWEED:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedTumbleweed();
                break;
        }
    }
}
