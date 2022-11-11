using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatStroke : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Mosquitoes>().attackMultiplayer = enemy.GetComponent<Mosquitoes>().attackMultiplayer * 0.75f;
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
