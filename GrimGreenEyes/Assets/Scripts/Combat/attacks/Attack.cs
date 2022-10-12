using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public new string name;
    public int power;
    public int accuraty;


    public float freezeDamage = 0.7f;
    public float heatDamage = 0.3f;

    public int DamageCalculator(Plants enemy, Plants player)
    {

        float totalDefense = enemy.defense +
            (
            enemy.heatResistance * heatDamage
            ) + (
            enemy.freezeResistance * freezeDamage
            );
        float damage =  (player.attack + power) /totalDefense;
        return (int)damage;
    }
    public virtual void Effect(GameObject ally, GameObject player) { }
}
