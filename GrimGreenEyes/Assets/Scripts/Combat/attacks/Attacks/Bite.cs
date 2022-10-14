using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Mordisco");
        enemy.GetComponent<Plants>().livePoints -= DamageCalculator(enemy.GetComponent<Plants>(), player.GetComponent<Plants>()); 
    }
}
