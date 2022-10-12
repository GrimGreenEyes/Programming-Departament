using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSap : Attack
{
    
    private void Awake()
    {
        name = "Magic Sap";
        power = 20;
        accuraty = 100;
    }
    public override void Effect(GameObject ally, GameObject player)
    {
        ally.GetComponent<Plants>().livePoints += player.GetComponent<Plants>().mainAttack.power;
         
    }
}
