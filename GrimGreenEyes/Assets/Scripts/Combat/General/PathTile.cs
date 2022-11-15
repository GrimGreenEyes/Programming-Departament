using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    public PathTile parent { get; private set; }
    public int weight { get; private set; }
    public int distance { get; private set; }
    public bool selected;

    public PathTile Init(int newWeight, PathTile newParent = null)
    {
        parent = newParent;
        weight = (parent != null) ? parent.weight + newWeight : newWeight;
        selected = true;
        return this;
    }
    private void Update()
    {
        if(GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.MOVEING)
        {
            parent = null;
            weight = GetComponent<Tile>().weight;
            selected = false;
            }
    }
}
