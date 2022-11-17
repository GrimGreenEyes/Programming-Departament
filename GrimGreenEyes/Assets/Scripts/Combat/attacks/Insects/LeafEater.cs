using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafEater : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        new WaitForSeconds(1f);
    }
}
    
