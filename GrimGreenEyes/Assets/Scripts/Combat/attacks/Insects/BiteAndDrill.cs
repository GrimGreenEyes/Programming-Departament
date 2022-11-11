using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAndDrill : Attack
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        enemy.GetComponent<Entity>().livePoints -= DamageCalculator(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        CheckDead(enemy.GetComponent<Entity>());
        SearchBack(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
    }
    private void SearchBack(Entity enemy, Entity player)
    {
        int positionX = enemy.gridX - player.gridX;
        int positionY = enemy.gridY - player.gridY;
        Entity otherEnemy = (GridCreator.instance.GetTile(player.gridX - positionX, player.gridY - positionX).GetComponent<Tile>().entity != null) ? GridCreator.instance.GetTile(player.gridX - positionX, player.gridY - positionX).GetComponent<Tile>().entity.GetComponent<Entity>() : null;
        if (otherEnemy == null)
        {
            return;
        }
        otherEnemy.GetComponent<Entity>().livePoints -= DamageCalculator(otherEnemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        CheckDead(otherEnemy);
    }
}
