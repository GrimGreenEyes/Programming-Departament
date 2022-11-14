using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAndTangle : Attack
{
    const float STUN_PERCENTAGE = 30;
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().livePoints -= DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        CheckDead(enemy.GetComponent<Entity>());
        if (Random.Range(0, 100) < STUN_PERCENTAGE)
        {
            enemy.GetComponent<Entity>().stuned = true;
        }
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
    }
}
