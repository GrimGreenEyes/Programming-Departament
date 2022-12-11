using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public new string name;
    public int range;
    public int radious;
    public bool directedToAlly;

    public float freezeDamage = 0.7f;
    public float heatDamage = 0.3f;

    private const int chargeDamage = 5;

    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] protected GameObject attackAnimation;
    public int DamageCalculator(Entity enemy, Entity player, int charges = 0)
    {
        Debug.Log(player.attack);
        Debug.Log("****");
        float totalDefense = enemy.defense +
            (
            enemy.heatResistance * heatDamage
            ) + (
            enemy.freezeResistance * freezeDamage
            );
        float damage =  (player.attack + chargeDamage * charges) * totalDefense / 100f;
        audioSource.Play();
        return (int)damage;
    }
    
    public virtual void Effect(GameObject objective, GameObject player) {
        Debug.Log("ataque base");
    }
}
