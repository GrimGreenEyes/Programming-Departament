using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().livePoints -= DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        CheckDead(enemy.GetComponent<Entity>());
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
    }
}
