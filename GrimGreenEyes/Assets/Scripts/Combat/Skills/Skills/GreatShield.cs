using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatShield : Skill
{
    private void GiveBuff(Tile tile)
    {
        if (tile.entity != null)
        {
            if (tile.entity.tag == "Player")
            {
                tile.entity.GetComponent<Plants>().defenseMultiplayer = tile.entity.GetComponent<Plants>().defenseMultiplayer * 2;
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

        tile = GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX, player.GetComponent<Plants>().gridY + 1).GetComponent<Tile>();
        GiveBuff(tile);
        
        tile = GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX, player.GetComponent<Plants>().gridY - 1).GetComponent<Tile>();
        GiveBuff(tile);
        
        tile = GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX + 1, player.GetComponent<Plants>().gridY).GetComponent<Tile>();
        GiveBuff(tile);
        
        tile = GridCreator.instance.GetTile(player.GetComponent<Plants>().gridX - 1, player.GetComponent<Plants>().gridY).GetComponent<Tile>();
        GiveBuff(tile);

        DeactivateSkill(player.GetComponent<Plants>());
    }
}
