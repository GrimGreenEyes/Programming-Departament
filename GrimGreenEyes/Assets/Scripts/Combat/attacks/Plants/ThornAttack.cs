using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : Attack
{
    
    public override void Effect(GameObject enemy, GameObject player)
    {

        GetEnemys(enemy.GetComponent<Entity>(), player.GetComponent<Entity>());
        player.GetComponent<Entity>().actualState = Entity.EntityState.IDLE;

    }
    private void GetEnemys(Entity enemy, Entity player)
    {
        
        int distanceX = enemy.gridX - player.gridX;
        int distanceY = enemy.gridY - player.gridY;
        Debug.Log(distanceX);
        Debug.Log(distanceY);
        GameObject animation1 = Instantiate(attackAnimation, enemy.transform);
        animation1.GetComponent<AttackAnimation>().Animate(6);
        enemy.GetComponent<Bichous>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
        GameObject newEnemy;
        if (distanceX != 0)
        {
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX, player.gridY + 1).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX, player.gridY - 1).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX, enemy.gridY + 1).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX, enemy.gridY - 1).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
        }
        else if (distanceY != 0)
        {
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX + 1, player.gridY).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
            if ((newEnemy = GridCreator.instance.GetTile(player.gridX - 1, player.gridY).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX + 1, enemy.gridY).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
            if ((newEnemy = GridCreator.instance.GetTile(enemy.gridX - 1, enemy.gridY).GetComponent<Tile>().entity) != null && newEnemy.tag == "Enemy")
            {
                GameObject animation = Instantiate(attackAnimation, newEnemy.transform);
                animation.GetComponent<AttackAnimation>().Animate(6);
                newEnemy.GetComponent<Entity>().Damage(DamageCalculator(enemy.GetComponent<Bichous>(), player.GetComponent<Plants>()));
            }
        }
    }
}
