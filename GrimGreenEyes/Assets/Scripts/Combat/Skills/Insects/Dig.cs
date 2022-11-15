using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : Skill
{
    private GameObject startingTile;
    private bool arrived = false;
    private float speed = 3;

    public override void Effect(GameObject tile, GameObject player)
    {
        if (startingTile == null && destinationTile == null)
        {
            arrived = false;
            destinationTile = tile;
            startingTile = player.GetComponent<Entity>().thisTile;
        }
        Move(player);
        if (arrived)
        {
            DigDamage(destinationTile.GetComponent<Tile>(), player.GetComponent<Entity>());
            destinationTile = null;
            startingTile = null;
            DeactivateSkill(player.GetComponent<Entity>());
        }
    }
    private void Move(GameObject player)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, destinationTile.transform.position + new Vector3(0f, 0.25f, 0f), speed * Time.deltaTime);
        if (player.transform.position == destinationTile.transform.position + new Vector3(0f, 0.25f, 0f))
        {
            arrived = true;
        }
    }
    private void DigDamage(Tile tile, Entity player)
    {
        Tile nextTile = GridCreator.instance.GetTile(tile.GetX() + 1, tile.GetY()).GetComponent<Tile>();
        if (nextTile.entity != null)
        {
            nextTile.entity.GetComponent<Entity>().Damage(DamageCalculator(nextTile.entity.GetComponent<Entity>(), player)/4);
        }
        nextTile = GridCreator.instance.GetTile(tile.GetX() - 1, tile.GetY()).GetComponent<Tile>();
        if (nextTile.entity != null)
        {
            nextTile.entity.GetComponent<Entity>().Damage(DamageCalculator(nextTile.entity.GetComponent<Entity>(), player)/4);
        }
        nextTile = GridCreator.instance.GetTile(tile.GetX(), tile.GetY() + 1).GetComponent<Tile>();
        if (nextTile.entity != null)
        {
            nextTile.entity.GetComponent<Entity>().Damage(DamageCalculator(nextTile.entity.GetComponent<Entity>(), player)/4);
        }
        nextTile = GridCreator.instance.GetTile(tile.GetX(), tile.GetY() - 1).GetComponent<Tile>();
        if (nextTile.entity != null)
        {
            nextTile.entity.GetComponent<Entity>().Damage(DamageCalculator(nextTile.entity.GetComponent<Entity>(), player)/4);
        }
    }
}
