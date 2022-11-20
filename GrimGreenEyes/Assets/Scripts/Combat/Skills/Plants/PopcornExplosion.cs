using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornExplosion : Skill
{
    public override void Effect(GameObject tile, GameObject player)
    {
        for (int x = 0; x < GridCreator.instance.width; x++)
        {
            for(int y = 0; y < GridCreator.instance.height; y++)
            {
                if ((Mathf.Abs(tile.GetComponent<Tile>().GetX()- x) + Mathf.Abs(tile.GetComponent<Tile>().GetY()- y)) <= areaOfEffect)
                {
                    SetPopcorn(GridCreator.instance.GetTile(x, y).GetComponent<Tile>());
                }
            }
        }
        DeactivateSkill(player.GetComponent<Plants>());
    }
    private void SetPopcorn(Tile tile)
    {
        tile.initTurn = -1;
        tile.isPopCorned = true;
    }
}
