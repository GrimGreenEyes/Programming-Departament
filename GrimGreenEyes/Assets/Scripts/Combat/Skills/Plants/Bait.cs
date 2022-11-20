using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        enemy.GetComponent<Entity>().movement = 0;
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        DeactivateSkill(player.GetComponent<Plants>());
        player.GetComponent<Entity>().actualState = Entity.EntityState.FINISHED;
    }
}
