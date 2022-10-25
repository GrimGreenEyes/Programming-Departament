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
    Dictionary<GameObject, int> openSetDictionary = new Dictionary<GameObject, int>();
    List<GameObject> closeSet = new List<GameObject>();
    List<GameObject> road = new List<GameObject>();
    Dictionary<GameObject, GameObject> parents = new Dictionary<GameObject, GameObject>();

    //int G = 0;
    int H = 0;
    //int F = 0;
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
    public List<GameObject> PathFind(GameObject start, GameObject destination)
    {
        openSet.Clear();
        closeSet.Clear();
        parents.Clear();
        road.Clear();
        parents.Add(start, null);
        openSet.Add(start);
        finish = destination;
        H = Mathf.Abs(finish.GetComponent<Tile>().GetX() - start.GetComponent<Tile>().GetX()) + Mathf.Abs(finish.GetComponent<Tile>().GetY() - start.GetComponent<Tile>().GetY());
        FindPath(start);
        this.start = start;
        return road;
        
    }
    public void PathShine(GameObject start)
    {
        openSet.Clear();
        closeSet.Clear();
        openSetDictionary.Clear();
        road.Clear();
        openSetDictionary.Add(start, 0);
        openSet.Add(start);
        ShinePath(start, -1);
        this.start = start;
    }
    private void FindPath(GameObject thisTile)
    {
        start = thisTile;
        openSet = openSet.OrderBy(tile => tile.GetComponent<Tile>().weight).ToList();
        
        closeSet.Add(openSet[0]);
        openSet.Remove(thisTile);
        if(thisTile == finish)
        {
            for (int i = 0; i < GameController.instance.SelectedPlayer().GetComponent<Plants>().movement; i++)
            {
                road.Add(thisTile);
                thisTile = parents[thisTile];
                if(parents[thisTile] == null)
                {
                    return;
                }
            }

        }
        if (thisTile.GetComponent<Tile>().GetX() + 1 < GridCreator.instance.width - 1 && !closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()));
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if (thisTile.GetComponent<Tile>().GetX() - 1 >= 0 && (!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY())) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()));
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if (thisTile.GetComponent<Tile>().GetY() + 1 < GridCreator.instance.height - 1 && !closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1)) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1))  && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1));
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if (thisTile.GetComponent<Tile>().GetY() - 1 >= 0 && (!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1)) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<Tile>().isWalkable)
        {
            openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1));
            parents.Add(openSet[openSet.Count() - 1], thisTile);
        }
        if (openSet.Count != 0)
        {
            FindPath(openSet[0]);
        }
        return;
    }

    private void ShinePath(GameObject thisTile, int position)
    {        
        thisTile.GetComponent<Tile>().isInRange = true;
        if (openSetDictionary[thisTile] < GameController.instance.SelectedPlayer().GetComponent<Plants>().movement)
        {
            if (thisTile.GetComponent<Tile>().GetX() + 1 < GridCreator.instance.width - 1 && !closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && !openSetDictionary.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
            {
                openSetDictionary.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()), openSetDictionary[thisTile] + 1);
                openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()));
                
            }
            if (thisTile.GetComponent<Tile>().GetX() - 1 >= 0 && (!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY())) && !openSetDictionary.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
            {
                openSetDictionary.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()), openSetDictionary[thisTile] + 1);
                openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()));
                
            }
            if (thisTile.GetComponent<Tile>().GetY() + 1 < GridCreator.instance.height - 1 && !closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1)) && !openSetDictionary.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1)) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<Tile>().isWalkable)
            {
                openSetDictionary.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1), openSetDictionary[thisTile] + 1);
                openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1));
                
            }
            if (thisTile.GetComponent<Tile>().GetY() - 1 >= 0 && (!closeSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1)) && !openSetDictionary.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<Tile>().isWalkable)
            {
                openSetDictionary.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1), openSetDictionary[thisTile] + 1);
                openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1));
                
            }
        }
        openSet.Remove(thisTile);
        if (openSet.Count != 0)
        {
            ShinePath(openSet[0], position++);
        }
        return;
    }
    private void RemoveObject(GameObject thisObject)
    {
        openSetDictionary.Remove(thisObject);
        foreach (KeyValuePair<GameObject, int> kvp in openSetDictionary)
        {
             
        }
    }

    void ConsoleOutput()
    {
        foreach (KeyValuePair<GameObject, int> kvp in openSetDictionary)  
            Debug.Log("Key = {0} + Value = {1}" + kvp.Key + kvp.Value);
    }
}

