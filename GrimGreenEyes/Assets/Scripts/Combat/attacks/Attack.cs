using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public new string name;
    public int range;
    public int radious;


    public float freezeDamage = 0.7f;
    public float heatDamage = 0.3f;

    public int DamageCalculator(Mosquitoes enemy, Plants player)
    {

        float totalDefense = enemy.defense +
            (
            enemy.heatResistance * heatDamage
            ) + (
            enemy.freezeResistance * freezeDamage
            );
        float damage =  (player.attack) * totalDefense / 100f;
        return (int)damage;
    }
    public virtual void Effect(GameObject objective, GameObject player) {
        Debug.Log("ataque base");
    }
}
