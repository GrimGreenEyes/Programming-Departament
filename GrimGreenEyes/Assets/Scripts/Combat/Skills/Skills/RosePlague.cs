using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosePlague : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        
        DeactivateSkill(player.GetComponent<Plants>());
    }
}
