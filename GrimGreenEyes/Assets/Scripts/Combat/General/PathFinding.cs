using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{
    public static PathFinding instance;

    GameObject start;
    GameObject finish;

 
    List<PathTile> openSet = new List<PathTile>();
    Dictionary<GameObject, int> openSetDictionary = new Dictionary<GameObject, int>();
    List<GameObject> openSetGO= new List<GameObject>();
    List<PathTile> closeSet = new List<PathTile>();
    List<GameObject> closeSetGO = new List<GameObject>();
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
        openSetDictionary.Clear();
        parents.Add(start, null);
        openSetDictionary.Add(start, 0);
        openSet.Add(start.GetComponent<PathTile>().Init(start.GetComponent<Tile>().weight));
        finish = destination;
        //H = Mathf.Abs(finish.GetComponent<Tile>().GetX() - start.GetComponent<Tile>().GetX()) + Mathf.Abs(finish.GetComponent<Tile>().GetY() - start.GetComponent<Tile>().GetY());
        FindPath();
        this.start = start;
        return road;
        
    }
    public void PathShine(GameObject start)
    {
        openSet.Clear();
        openSetGO.Clear();
        closeSet.Clear();
        closeSetGO.Clear();
        openSetDictionary.Clear();
        road.Clear();
        openSetDictionary.Add(start, 0);
        //openSet.Add(new Node(start, start.GetComponent<Tile>().weight));
        openSetGO.Add(start);
        ShinePath(start, -1);
        this.start = start;
        return;
    }
    private void FindPath()
    {
        PathTile thisTile = openSet[0];
        thisTile.GetComponent<Tile>().isInRange = true;
        PathTile nextTile;
        for(int i = 0; openSet.Count > 0 ; i++)
        {
            thisTile = openSet[0];
            if (openSet[0].distance < GameController.instance.SelectedPlayer().GetComponent<Entity>().movement)
            {
                if(thisTile.GetComponent<Tile>().GetY() - 1 >= 0 && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<Tile>().isWalkable)
                {
                    if (!GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<PathTile>().selected)
                    {
                        nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<PathTile>().Init(thisTile.GetComponent<Tile>().weight, openSet[0]);
                        if (!closeSet.Contains(nextTile) && !openSet.Contains(nextTile) && nextTile.GetComponent<Tile>().isWalkable)
                        {
                            if(GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.gameObject.tag == "PathTile")
                            {
                                openSet.Add(nextTile);
                            }
                            else if(GameController.instance.SelectedPlayer().tag != "Carriage")
                            {
                                openSet.Add(nextTile);
                            }
                        }
                    }
                }
                if (thisTile.GetComponent<Tile>().GetY() + 1 < GridCreator.instance.height && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<Tile>().isWalkable)
                {
                    if (!GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<PathTile>().selected)
                    {
                        nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<PathTile>().Init(thisTile.GetComponent<Tile>().weight, openSet[0]);
                        if (!closeSet.Contains(nextTile) && !openSet.Contains(nextTile) && nextTile.GetComponent<Tile>().isWalkable)
                        {
                            if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.gameObject.tag == "PathTile")
                            {
                                openSet.Add(nextTile);
                            }
                            else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                            {
                                openSet.Add(nextTile);
                            }
                        }
                    }
                }
                if(thisTile.GetComponent<Tile>().GetX() + 1 < GridCreator.instance.width && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
                {
                    Debug.Log("path " + i);
                    if (!GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<PathTile>().selected)
                    {
                        nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<PathTile>().Init(thisTile.GetComponent<Tile>().weight, openSet[0]);                  
                        if (!closeSet.Contains(nextTile) && !openSet.Contains(nextTile) && nextTile.GetComponent<Tile>().isWalkable)
                        {
                            if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.gameObject.tag == "PathTile")
                            {
                                openSet.Add(nextTile);
                            }
                            else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                            {
                                openSet.Add(nextTile);
                            }
                        }
                    }
                }
                if (thisTile.GetComponent<Tile>().GetX() - 1 >= 0 && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
                {
                    if (!GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<PathTile>().selected)
                    {
                        nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<PathTile>().Init(thisTile.GetComponent<Tile>().weight, openSet[0]);
                        if (!closeSet.Contains(nextTile) && !openSet.Contains(nextTile) && nextTile.GetComponent<Tile>().isWalkable)
                        {
                            if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.gameObject.tag == "PathTile")
                            {
                                openSet.Add(nextTile);
                            }
                            else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                            {
                                openSet.Add(nextTile);
                            }
                        }
                    }
                }
            }
            if(thisTile.gameObject == finish)
            {
                
                for (int j = 0; j < GameController.instance.SelectedPlayer().GetComponent<Entity>().movement; j++)
                {
        
                    if(thisTile.parent == null) { return; }
                    road.Add(thisTile.gameObject);
                    thisTile = thisTile.parent;
        
                }
            }
            closeSet.Add(openSet[0]);
            openSet.RemoveAt(0);
            openSet = openSet.OrderBy(tile => tile.weight).ToList();
        }
        return;
    
    //    start = thisTile;
    //    
    //    closeSet.Add(openSet[0]);
    //    openSet.Remove(thisTile);
    //    if(thisTile == finish)
    //    {
    //        for (int i = 0; i < GameController.instance.SelectedPlayer().GetComponent<Entity>().movement; i++)
    //        {
    //            road.Add(thisTile);
    //            thisTile = parents[thisTile];
    //            if(parents[thisTile] == null)
    //            {
    //                return;
    //            }
    //        }
    //
    //    }
    //    if (thisTile.GetComponent<Tile>().GetX() + 1 < GridCreator.instance.width - 1 && !parents.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY())) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
    //    {
    //        openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()));
    //        parents.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY()), thisTile);
    //    }
    //    if (thisTile.GetComponent<Tile>().GetX() - 1 >= 0 && (!parents.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY())) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()).GetComponent<Tile>().isWalkable)
    //    {
    //        openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()));
    //        parents.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY()), thisTile);
    //    }
    //    if (thisTile.GetComponent<Tile>().GetY() + 1 < GridCreator.instance.height - 1 && !parents.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1)) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1))  && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1).GetComponent<Tile>().isWalkable)
    //    {
    //        openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1));
    //        parents.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1), thisTile);
    //    }
    //    if (thisTile.GetComponent<Tile>().GetY() - 1 >= 0 && (!parents.ContainsKey(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1)) && !openSet.Contains(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1))) && GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1).GetComponent<Tile>().isWalkable)
    //    {
    //        openSet.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1));
    //        parents.Add(GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1), thisTile);
    //    }
    //    openSet = openSet.OrderBy(tile => tile.GetComponent<Tile>().weight).ToList();
    //    if (openSet.Count != 0)
    //    {
    //        FindPath(openSet[0]);
    //    }
    //    return;
    }

    private void ShinePath(GameObject thisTile, int position)
    {
        thisTile.GetComponent<Tile>().isInRange = true;
        GameObject nextTile;
        if (openSetDictionary[thisTile] < GameController.instance.SelectedPlayer().GetComponent<Entity>().movement)
        {
            if (thisTile.GetComponent<Tile>().GetX() + 1 <= GridCreator.instance.width - 1)
            {
                nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() + 1, thisTile.GetComponent<Tile>().GetY());
                if ((!closeSetGO.Contains(nextTile) && !openSetDictionary.ContainsKey(nextTile)) && nextTile.GetComponent<Tile>().isWalkable)
                {
                    if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.tag == "PathTile")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }
                    else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }
                }
            }
            
            if (thisTile.GetComponent<Tile>().GetX() - 1 >= 0)
            {
                nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX() - 1, thisTile.GetComponent<Tile>().GetY());
                if ((!closeSetGO.Contains(nextTile) && !openSetDictionary.ContainsKey(nextTile)) && nextTile.GetComponent<Tile>().isWalkable)
                {
                    if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.tag == "PathTile")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }
                    else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }

                }
            }
            
            if (thisTile.GetComponent<Tile>().GetY() + 1 <= GridCreator.instance.height - 1)
            {
                nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() + 1);
                if ((!closeSetGO.Contains(nextTile) && !openSetDictionary.ContainsKey(nextTile)) && nextTile.GetComponent<Tile>().isWalkable)
                {
                    if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.tag == "PathTile")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }
                    else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }

                }
            }
            
            if (thisTile.GetComponent<Tile>().GetY() - 1 >= 0)
            {
                nextTile = GridCreator.instance.GetTile(thisTile.GetComponent<Tile>().GetX(), thisTile.GetComponent<Tile>().GetY() - 1);
                if ((!closeSetGO.Contains(nextTile) && !openSetDictionary.ContainsKey(nextTile)) && nextTile.GetComponent<Tile>().isWalkable)
                {
                    if (GameController.instance.SelectedPlayer().tag == "Carriage" && nextTile.tag == "PathTile")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }
                    else if (GameController.instance.SelectedPlayer().tag != "Carriage")
                    {
                        openSetDictionary.Add(nextTile, openSetDictionary[thisTile] + 1);
                        openSetGO.Add(nextTile);
                    }

                }
            }
        }
        openSetGO.Remove(thisTile);
        if (openSetGO.Count != 0)
        {
            ShinePath(openSetGO[0], position++);
        }
        return; 
        
    }
    
}

