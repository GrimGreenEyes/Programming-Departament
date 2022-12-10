using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropedObject : MonoBehaviour
{
    public new string name;
    private enum Seeds { VERA, LILY, SUNFLOWER, CACTUS, ROSE, FUNGUS, CORN, TUMBLEWEED}
    [SerializeField]private Seeds seed;
    private GameObject picker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Feet")
        {
            return;
        }
        GameObject thisEntity = collision.transform.parent.gameObject;
        if (thisEntity.tag == "Player")
        {
            picker = thisEntity;
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
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Vera", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Vera");
                break;
            case Seeds.LILY:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedLily();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Belator", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Belator");
                break;
            case Seeds.SUNFLOWER:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedSunflower();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Lumendus", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Lumendus");
                break;
            case Seeds.ROSE:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedRose();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Sartiry", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Sartiry");
                break;
            case Seeds.FUNGUS:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedCarnivorousFungus();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Toxkill", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Toxkill");
                break;
            case Seeds.CACTUS:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedCactus();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Skarkmor", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Skarkmor");
                break;
            case Seeds.CORN:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedCorn();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Zemays", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Zemays");
                break;
            case Seeds.TUMBLEWEED:
                GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>().AddSeedTumbleweed();
                GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(picker.GetComponent<Entity>(), "+1 Semilla de Rolay", Color.cyan, 2.3f);
                GameObject.Find("GameController").GetComponent<GameController>().AddResource("Semilla de Rolay");
                break;
        }
    }
}
