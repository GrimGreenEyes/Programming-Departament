using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Skill
{
    private GameObject startingTile;
    private bool arrived = false;
    private float speed = 4;

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
}
