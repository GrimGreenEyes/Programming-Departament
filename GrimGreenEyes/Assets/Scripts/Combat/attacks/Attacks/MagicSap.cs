using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSap : Attack
{
    
    private void Awake()
    {
        name = "Magic Sap";
    }
    public override void Effect(GameObject ally, GameObject player)
    {
        Debug.Log("Curación a " + ally.name);
        ally.GetComponent<Plants>().livePoints += player.GetComponent<Plants>().attack;
         
    }
}
