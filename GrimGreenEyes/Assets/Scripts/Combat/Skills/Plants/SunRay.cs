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
        ally.GetComponent<Plants>().defenseMultiplayer = ally.GetComponent<Plants>().defenseMultiplayer * 2f;
        DeactivateSkill(player.GetComponent<Plants>());
    }
}
