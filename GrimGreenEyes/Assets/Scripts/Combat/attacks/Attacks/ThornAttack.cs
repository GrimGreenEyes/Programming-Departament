using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : Attack
{
    
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Ataque cactus");
        enemy.GetComponent<Mosquitoes>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());

    }
}
