using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bite : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
    }
}
