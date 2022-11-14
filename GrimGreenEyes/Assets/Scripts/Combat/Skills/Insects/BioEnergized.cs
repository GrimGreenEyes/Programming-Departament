using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioEnergized : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        player.GetComponent<Entity>().charges++;
    }
}
