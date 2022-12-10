using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMsgManager : MonoBehaviour
{
    [SerializeField] private GameObject statusMsgPrefab;

    public void ShowMsg(Entity entity, string text, Color color, float duration)
    {
       StatusMsg status = Instantiate(statusMsgPrefab).GetComponent<StatusMsg>();

            foreach (Transform child in EntityFeet(entity).transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }

        GameObject parent = EntityFeet(entity).transform.GetChild(0).gameObject;

        status.DisplayMsg(parent, text, color, duration);
    }

    private GameObject EntityFeet(Entity entity)
    {
        GameObject feet = null;

        try { feet = GameObject.Find(entity.gameObject.name + "/Canvas/Feet"); } catch { }

        if(feet != null)
        {
            return feet;
        }
        else
        {
            try { feet = GameObject.Find(entity.gameObject.name + "/Feet"); } catch { }
        }
        Debug.Log("EL PIE ES: " + feet);
        return feet;
    }
}
