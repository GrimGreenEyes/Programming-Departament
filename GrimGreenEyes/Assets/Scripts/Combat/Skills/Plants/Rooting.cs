using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooting : Skill
{
    public Color color;
    public override void Effect(GameObject enemy, GameObject player)
    {
        if (alreadyUsed)
        {
            DeactivateSkill(player.GetComponent<Plants>());
            return;
        }

        DeactivateSkill(player.GetComponent<Plants>());
        player.GetComponent<Plants>().Hide(color);
    }
}
