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
        enemy.GetComponent<Bichous>().Damage(damage);
        player.GetComponent<Plants>().Heal((int)(damage * liveRegen));
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(6);
        GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
    }
}
