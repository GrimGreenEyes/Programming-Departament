using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatShield : Skill
{
    private void GiveBuff(Tile tile)
    {
        if(tile == null)
        {
            return;
        }
        if (tile.entity != null)
        {
            if (tile.entity.tag == "Player")
            {
                tile.entity.GetComponent<Entity>().DefenseBust(20);
            }
        }
    }
    public override void Effect(GameObject enemy, GameObject player)
    {
        if (alreadyUsed)
        {
            DeactivateSkill(player.GetComponent<Plants>());
            return;
        }
        Tile tile = GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX, player.GetComponent<Plants>().gridY).GetComponent<Tile>();
        GiveBuff(tile);

        tile = (player.GetComponent<Plants>().gridY + 1 < GridCreator.instance.height) ? GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX, player.GetComponent<Plants>().gridY + 1).GetComponent<Tile>() : null;
        GiveBuff(tile);
        
        tile = (player.GetComponent<Plants>().gridY - 1 >= 0) ? GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX, player.GetComponent<Plants>().gridY - 1).GetComponent<Tile>() : null;
        GiveBuff(tile);
        
        tile = (player.GetComponent<Plants>().gridX + 1 < GridCreator.instance.width) ? GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX + 1, player.GetComponent<Plants>().gridY).GetComponent<Tile>() : null;
        GiveBuff(tile);
        
        tile = (player.GetComponent<Plants>().gridX - 1 >= 0) ? GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX - 1, player.GetComponent<Plants>().gridY).GetComponent<Tile>() : null;
        GiveBuff(tile);
        GameObject animation = Instantiate(attackAnimation, player.transform);
        animation.GetComponent<AttackAnimation>().Animate(3);
        DeactivateSkill(player.GetComponent<Plants>());
    }
}
