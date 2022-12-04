using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMind : Skill
{
    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("NIBUS");
        Tile tile = player.GetComponent<Entity>().thisTile.GetComponent<Tile>();

        bool spawned = false;
        if (!spawned)
        {
            try
            {
                tile = GridCreator.instance.GetTile(tile.GetX() + 1, tile.GetY()).GetComponent<Tile>();
                spawned = true;
                player.GetComponent<Entity>().gottenSeed = true;


            }
            catch
            {
                spawned = false;
            }
        }
        if (!spawned)
        {
            try
            {
                tile = GridCreator.instance.GetTile(tile.GetX() - 1, tile.GetY()).GetComponent<Tile>();
                spawned = true;
                player.GetComponent<Entity>().gottenSeed = true;

            }
            catch
            {
                spawned = false;
            }
        }
        if (!spawned)
        {
            try
            {
                tile = GridCreator.instance.GetTile(tile.GetX(), tile.GetY() + 1).GetComponent<Tile>();
                spawned = true;
                player.GetComponent<Entity>().gottenSeed = true;

            }
            catch
            {
                spawned = false;
            }
        }
        if (!spawned)
        {
            try
            {
                tile = GridCreator.instance.GetTile(tile.GetX(), tile.GetY() - 1).GetComponent<Tile>();
                spawned = true;
                player.GetComponent<Entity>().gottenSeed = true;

            }
            catch
            {
                spawned = false;
            }
        }
        /*
        switch ((int)Random.Range(0, 4))
        {
            case 0:
                tile = GridCreator.instance.GetTile(tile.GetX() + 1, tile.GetY()).GetComponent<Tile>();
                break;
            case 1:
                tile = GridCreator.instance.GetTile(tile.GetX() - 1, tile.GetY()).GetComponent<Tile>();
                break;
            case 2:
                tile = GridCreator.instance.GetTile(tile.GetX(), tile.GetY() + 1).GetComponent<Tile>();
                break;
            case 3:
                tile = GridCreator.instance.GetTile(tile.GetX(), tile.GetY() - 1).GetComponent<Tile>();
                break;
        }
        */
        GameController.instance.AddEnemy(player, tile.gameObject.transform);
        player.GetComponent<Entity>().actualState = Entity.EntityState.MOVEING;
    }
}
