using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().bleeding = true;
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(6);
        new WaitForSeconds(1f);
    }
}
