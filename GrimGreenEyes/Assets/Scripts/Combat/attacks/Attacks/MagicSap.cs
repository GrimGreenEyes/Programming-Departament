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
        if(ally.tag != "Player")
        {
            player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
            return;
        }
        ally.GetComponent<Plants>().livePoints = (ally.GetComponent<Plants>().livePoints + player.GetComponent<Plants>().attack < ally.GetComponent<Plants>().maxLivePoints) ? ally.GetComponent<Plants>().maxLivePoints + player.GetComponent<Plants>().attack : ally.GetComponent<Plants>().maxLivePoints;
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
