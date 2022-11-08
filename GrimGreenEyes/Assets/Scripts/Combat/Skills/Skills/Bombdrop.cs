using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombdrop : Skill
{
    private GameObject startingTile;
    private List<Tile> affectedTiles;
    private float speed = 8;
    
    private void GetTiles()
    {
        if(destinationTile.GetComponent<Tile>().GetX() == startingTile.GetComponent<Tile>().GetX())
        {
            for (int i = 0; i < Mathf.Abs(destinationTile.GetComponent<Tile>().GetY() - startingTile.GetComponent<Tile>().GetY()); i++)
            {
                affectedTiles.Add(GridCreator.instance.GetTile(destinationTile.GetComponent<Tile>().GetX(), (destinationTile.GetComponent<Tile>().GetY() - startingTile.GetComponent<Tile>().GetY() < 0) ? -i : i).GetComponent<Tile>());
            }
        }
    }
    public override void Effect(GameObject tile, GameObject player)
    {
        if(startingTile == null)
        {
            destinationTile = tile;
            startingTile = player.GetComponent<Plants>().thisTile;
            GetTiles();
        }
        Move(player);
        if(player.transform.position == destinationTile.transform.position + new Vector3(0f, 0.25f, 0f))
        {
            DropBomb(player);
            startingTile = null;
            DeactivateSkill(player.GetComponent<Plants>());
        }
    }
    private void Move(GameObject player)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, destinationTile.transform.position + new Vector3(0f, 0.25f, 0f), speed);
    }
    private void DropBomb(GameObject player)
    {
        for (int i = 0; i < affectedTiles.Count; i++)
        {
            if(affectedTiles[i].entity != null)
            {
                DamageCalculator(affectedTiles[i].entity.GetComponent<Mosquitoes>(), player.GetComponent<Plants>());
                
            }
        }
    }
}
