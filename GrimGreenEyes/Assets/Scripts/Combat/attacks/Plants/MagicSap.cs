using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSap : Attack
{
    public override void Effect(GameObject ally, GameObject player)
    {
        if(ally.tag != "Player")
        {
            player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
            return;
        }
        ally.GetComponent<Plants>().Heal(player.GetComponent<Plants>().attack); 
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
