using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{
    public static PathFinding instance;

    GameObject start;
    GameObject finish;

    List<GameObject> openSet = new List<GameObject>();
    List<GameObject> closeSet = new List<GameObject>();
    List<GameObject> road = new List<GameObject>();
    Dictionary<GameObject, GameObject> parents = new Dictionary<GameObject, GameObject>();

    int G = 0;
    int H = 0;
    int F = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnDestroy()
    {
        if (instance = this)
        {
            instance = null;
        }
    }
    public List<GameObject> AStar(GameObject start, GameObject destination)
    {
        openSet.Clear();
        closeSet.Clear();
        parents.Clear();
        road.Clear();
        parents.Add(start, null);
        openSet.Add(start);
        finish = destination;
        H = Mathf.Abs(finish.GetComponent<Tile>().GetX() - start.GetComponent<Tile>().GetX()) + Mathf.Abs(finish.GetComponent<Tile>().GetY() - start.GetComponent<Tile>().GetY());
        FindPath(start, -1);
        return road;
        /*foreach (GameObject i in openSet)
        {
            FindPath(i);
        }*/
    }
    private void FindPath(GameObject thisTile, int position)
    {
        start = thisTile;
        position++;

        
        closeSet.Add(openSet[0]);
        if(thisTile == finish)
        {
            ConsoleOutput();
            for (int i = 0; i < position; i++)
            {
                road.Add(thisTile);
                thisTile = parents[thisTile];
                if(parents[thisTile] == null)
                {
                    return;
                }
            }

        }
        openSet.Remove(thisTile);
        if ((!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()));
            //Debug.Log(openSet[openSet.Count() - 1]);
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if (thisTile.GetComponent<Tile>().GetX() - 1 >= 0 && (!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY())) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()));
            //Debug.Log(openSet[openSet.Count() - 1]);
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if ((!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1)) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1)))  && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1));
            //Debug.Log(openSet[openSet.Count() - 1]);
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if (thisTile.GetComponent<Tile>().GetY() - 1 >= 0 && (!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1)) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1));
            //Debug.Log(openSet[openSet.Count() - 1]);
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        openSet.OrderBy(w => w.GetComponent<Tile>().weight);
        if(openSet.Count != 0)
        {
            FindPath(openSet[0], position);
        }
        return;
    }
    void ConsoleOutput()
    {
        foreach (KeyValuePair<GameObject, GameObject> kvp in parents)
            Debug.Log("Key = {0} + Value = {1}" + kvp.Key + kvp.Value);
    }
}

