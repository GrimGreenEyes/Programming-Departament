using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAndTangle : Attack
{
    const float STUN_PERCENTAGE = 30;
    public override void Effect(GameObject enemy, GameObject player)
    {
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        if (Random.Range(0, 100) < STUN_PERCENTAGE)
        {
            enemy.GetComponent<Entity>().stuned = true;
        }
    }
}
