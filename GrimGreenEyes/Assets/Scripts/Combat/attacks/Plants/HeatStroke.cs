using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatStroke : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Bichous>().AttackBust(-10);
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(5);
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;

    }
}
