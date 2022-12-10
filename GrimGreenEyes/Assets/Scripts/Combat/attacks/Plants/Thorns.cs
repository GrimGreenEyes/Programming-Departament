using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(6);
        enemy.GetComponent<Entity>().bleeding = true;
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
    }
}
