using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaConJunco : Attack
{
    private float liveRegen = 0.5f;
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Lanza");
        int damage = DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
        enemy.GetComponent<Mosquitoes>().livePoints -= damage;
        player.GetComponent<Plants>().Heal((int)(damage * liveRegen));
        enemy.GetComponent<Entity>().CheckDead();
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
