using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosePlague : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(7);
        player.GetComponent<Entity>().Heal(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
    }
}
