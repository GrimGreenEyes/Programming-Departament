using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardThorns : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("activated skill");
        enemy.GetComponent<Entity>().livePoints -= 5;
        CheckDead(enemy.GetComponent<Entity>());
    }
}
