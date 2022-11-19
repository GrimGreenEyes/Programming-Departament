using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAndDrill : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        SearchBack(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        new WaitForSeconds(1f);
    }
    private void SearchBack(Entity enemy, Entity player)
    {
        int positionX = enemy.gridX - player.gridX;
        int positionY = enemy.gridY - player.gridY;
        Entity otherEnemy = (GridCreator.instance.GetTile(player.gridX - positionX, player.gridY - positionY).GetComponent<Tile>().entity != null) ? GridCreator.instance.GetTile(player.gridX - positionX, player.gridY - positionY).GetComponent<Tile>().entity.GetComponent<Entity>() : null;
        if (otherEnemy == null)
        {
            return;
        }
        otherEnemy.GetComponent<Entity>().Damage(DamageCalculator(otherEnemy.GetComponent<Entity>(), player.GetComponent<Entity>()));
        GameObject animation = Instantiate(attackAnimation, enemy.transform);
        animation.GetComponent<AttackAnimation>().Animate(4);
        enemy.GetComponent<Entity>().CheckDead();
    }
}
