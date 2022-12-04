using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipAndSaw : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        player.GetComponent<Entity>().attack += 5;
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>(), player.GetComponent<Entity>().charges));
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
    }
}
