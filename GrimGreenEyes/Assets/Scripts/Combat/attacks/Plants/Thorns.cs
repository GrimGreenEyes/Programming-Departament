using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Ataque rosa");
        enemy.GetComponent<Mosquitoes>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
        enemy.GetComponent<Mosquitoes>().bleeding = true;
        CheckDead(enemy.GetComponent<Mosquitoes>());
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
