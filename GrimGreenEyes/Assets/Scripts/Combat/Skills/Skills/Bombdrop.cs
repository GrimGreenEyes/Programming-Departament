using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombdrop : Skill
{
    private GameObject startingTile;
    //private GameObject destinationTile;
    private List<Tile> affectedTiles = new List<Tile>();
    private bool arrived = false;
    private float speed = 4;
    
    private void GetTiles()
    {
        if(destinationTile.GetComponent<Tile>().GetX() == startingTile.GetComponent<Tile>().GetX())
        {
            for (int i = 0; i < Mathf.Abs(destinationTile.GetComponent<Tile>().GetY() - startingTile.GetComponent<Tile>().GetY()); i++)
            {
                affectedTiles.Add(GridCreator.instance.GetTile(destinationTile.GetComponent<Tile>().GetX(), (destinationTile.GetComponent<Tile>().GetY() - startingTile.GetComponent<Tile>().GetY() < 0) ? startingTile.GetComponent<Tile>().GetY() - i : startingTile.GetComponent<Tile>().GetY() + i).GetComponent<Tile>());
            }
        }
        else if (destinationTile.GetComponent<Tile>().GetY() == startingTile.GetComponent<Tile>().GetY())
        {
            for (int i = 0; i < Mathf.Abs(destinationTile.GetComponent<Tile>().GetX() - startingTile.GetComponent<Tile>().GetX()); i++)
            {
                affectedTiles.Add(GridCreator.instance.GetTile((destinationTile.GetComponent<Tile>().GetX() - startingTile.GetComponent<Tile>().GetX() < 0) ?  startingTile.GetComponent<Tile>().GetX() - i : startingTile.GetComponent<Tile>().GetX() + i , destinationTile.GetComponent<Tile>().GetY()).GetComponent<Tile>());
            }
        }
    }
    public override void Effect(GameObject tile, GameObject player)
    {
        if(startingTile == null && destinationTile == null)
        {
            arrived = false;
            destinationTile = tile;
            startingTile = player.GetComponent<Plants>().thisTile;
            GetTiles();
        }
        Move(player);
        if (arrived)
        {
            DropBomb(player);
            affectedTiles.Clear();
            destinationTile = null;
            startingTile = null;
            DeactivateSkill(player.GetComponent<Plants>());
        }
    }
    private void Move(GameObject player)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, destinationTile.transform.position + new Vector3(0f, 0.25f, 0f), speed * Time.deltaTime);
        if(player.transform.position == destinationTile.transform.position + new Vector3(0f, 0.25f, 0f))
        {
            arrived = true;
        }
    }
    private void DropBomb(GameObject player)
    {
        for (int i = 0; i < affectedTiles.Count; i++)
        {
            if(affectedTiles[i].entity != null && affectedTiles[i].entity != player)
            {
                Debug.Log("Hay enemigo");
                affectedTiles[i].entity.GetComponent<Entity>().livePoints -= DamageCalculator(affectedTiles[i].entity.GetComponent<Entity>(), player.GetComponent<Entity>());
                CheckDead(affectedTiles[i].entity.GetComponent<Entity>());
                
            }
        }
    }
}
