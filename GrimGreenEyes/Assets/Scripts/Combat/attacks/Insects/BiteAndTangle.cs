using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAndTangle : Attack
{
    const float STUN_PERCENTAGE = 30;
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        if (Random.Range(0, 100) < STUN_PERCENTAGE)
        {
            enemy.GetComponent<Entity>().stuned = true;
        }
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        new WaitForSeconds(1f);
    }
}
