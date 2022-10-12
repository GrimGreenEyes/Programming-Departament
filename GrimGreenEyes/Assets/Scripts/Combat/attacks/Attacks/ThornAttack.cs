using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : Attack
{
    private void Awake()
    {
        name = "Thorn Attack";
        power = 50;
        accuraty = 100;
    }
    public override void Effect(GameObject ally, GameObject player)
    {
        ally.GetComponent<Plants>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(ally.GetComponent<Plants>(), player.GetComponent<Plants>());

    }
}
