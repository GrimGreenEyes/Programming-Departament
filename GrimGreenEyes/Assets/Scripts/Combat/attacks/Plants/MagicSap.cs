using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSap : Attack
{
    public override void Effect(GameObject ally, GameObject player)
    {
        if(ally.tag != "Player")
        {
            player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
            return;
        }
        ally.GetComponent<Plants>().Heal(player.GetComponent<Plants>().attack);
        GameObject animation = Instantiate(attackAnimation, ally.transform);
        animation.GetComponent<AttackAnimation>().Animate(0);
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;

    }
}
