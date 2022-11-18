using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaConJunco : Attack
{
    [SerializeField] private float liveRegen = 0.5f;
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Lanza");
        int damage = DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>());
        enemy.GetComponent<Bichous>().livePoints -= damage;
        player.GetComponent<Plants>().Heal((int)(damage * liveRegen));
        enemy.GetComponent<Entity>().CheckDead();
        if(enemy.transform.childCount < 3)
        {
            GameObject animation = Instantiate(attackAnimation, enemy.transform);
            animation.GetComponent<AttackAnimation>().Animate(6);
            new WaitForSeconds(1f);
        }
    }
}
