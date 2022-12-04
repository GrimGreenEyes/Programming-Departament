using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropedObject : MonoBehaviour
{
    public new string name;
    private enum Seeds { VERA, LILY, SUNFLOWER, CACTUS, ROSE, FUNGUS, CORN, TUMBLEWEED}
    [SerializeField]private Seeds seed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Feet")
        {
            return;
        }
        GameObject thisEntity = collision.transform.parent.gameObject;
        if (thisEntity.tag == "Player")
        {
            PickUpObject();
            GridCreator.instance.seeds.Remove(gameObject);
            Destroy(gameObject);
        }else
            for (int i = 0; i <  thisEntity.GetComponent<Entity>().skills.Count; i++)
            {
                if (thisEntity.GetComponent<Entity>().skills[i].isOnSeed)
                {
                    thisEntity.GetComponent<Entity>().skillSelected = i;
                    thisEntity.GetComponent<Entity>().actualState = Entity.EntityState.USINGSKILL;
                }
                if (thisEntity.GetComponent<Entity>().name == "Nibus")
                {
                    GridCreator.instance.seeds.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
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
