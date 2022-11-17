using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatStroke : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Bichous>().attackMultiplayer = enemy.GetComponent<Bichous>().attackMultiplayer * 0.75f;
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(5);
        new WaitForSeconds(1f);

    }
}
