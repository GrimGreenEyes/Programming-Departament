using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRay : Skill
{
    public override void Effect(GameObject ally, GameObject player)
    {
        Debug.Log("does this");
        if (alreadyUsed)
        {
            DeactivateSkill(player.GetComponent<Plants>());
            return;
        }
        ally.GetComponent<Plants>().defense += 10;
        GameObject animation = Instantiate(attackAnimation, ally.transform);
        animation.GetComponent<AttackAnimation>().Animate(2);
        DeactivateSkill(player.GetComponent<Plants>());
    }
}
