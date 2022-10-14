using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Ataque rosa");
        enemy.GetComponent<Plants>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Plants>(), player.GetComponent<Plants>());
        enemy.GetComponent<Plants>().bleeding = true;
    }
}
