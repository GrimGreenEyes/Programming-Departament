using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Mordisco");
        enemy.GetComponent<Mosquitoes>().livePoints -= DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>()); 
    }
}
