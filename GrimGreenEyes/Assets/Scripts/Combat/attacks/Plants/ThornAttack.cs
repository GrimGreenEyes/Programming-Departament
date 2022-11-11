using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : Attack
{
    
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("Ataque cactus");
        GetEnemys(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        CheckDead(enemy.GetComponent<Mosquitoes>());
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
    private void GetEnemys(Entity enemy, Entity player)
    {
        
        int distanceX = enemy.gridX - player.gridX;
        int distanceY = enemy.gridY - player.gridY;
        Debug.Log(distanceX);
        Debug.Log(distanceY);
        enemy.GetComponent<Mosquitoes>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
        GameObject newEnemy;
        if (distanceX != 0)
        {
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX, player.gridY + 1).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX, player.gridY - 1).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX, enemy.gridY + 1).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX, enemy.gridY - 1).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
        }
        else if (distanceY != 0)
        {
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX + 1, player.gridY).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX - 1, player.gridY).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX + 1, enemy.gridY).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX - 1, enemy.gridY).GetComponent<Tile>().entity) != null)
            {
                newEnemy.GetComponent<Entity>().livePoints -= player.GetComponent<Plants>().mainAttack.DamageCalculator(enemy.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
            }
        }
    }
}
