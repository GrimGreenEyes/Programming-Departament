using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxines : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        if(enemy.tag == "Enemy")
        {
            enemy.GetComponent<Entity>().Heal((int)((float)enemy.GetComponent<Entity>().maxLivePoints * 0.3f));
        }
        if(enemy.tag == "Player")
        {
            enemy.GetComponent<Entity>().poisoned = true;
        }
        DeactivateSkill(player.GetComponent<Entity>());
    }
}
