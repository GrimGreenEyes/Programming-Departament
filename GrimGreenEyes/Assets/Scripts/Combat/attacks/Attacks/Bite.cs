using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Mosquitoes>().livePoints -= DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
        CheckDead(enemy.GetComponent<Mosquitoes>());
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
