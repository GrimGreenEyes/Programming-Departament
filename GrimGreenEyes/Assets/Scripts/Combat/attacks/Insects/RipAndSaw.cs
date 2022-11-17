using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipAndSaw : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().livePoints -= DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>(), player.GetComponent<Entity>().charges);
        enemy.GetComponent<Entity>().CheckDead();
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        new WaitForSeconds(1f);
    }
}
