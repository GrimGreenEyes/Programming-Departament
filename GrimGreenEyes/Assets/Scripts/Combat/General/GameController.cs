using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public int characterSelected = -1;
    private List<GameObject> characters = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private List<int> playerLivePoints = new List<int>();
    public List<GameObject> enemys = new List<GameObject>();

    public GameObject winScreen;
    public GameObject looseScreen;
    public GameObject defaultWinButton;

    private TeamInfo teamManager;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnDestroy()
    {
         if(instance == this)
        {
            instance = null;
        }
    }
    private void Start()
    {
        //teamManager = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
    }

    public void Init(GameObject entity)
    {
        characters.Add(entity);
        if(entity.tag == "Player")
        {
            players.Add(entity);
            //playerLivePoints.Add(0);
        }
        else if(entity.tag == "Enemy")
        {
            enemys.Add(entity);
        }
    }
    public void AddEnemy(GameObject newEnemyPref, Transform position)
    {
        GameObject newEnemy = Instantiate(newEnemyPref, position);
        Init(newEnemy);
    }
    public void OrderCharacters()
    {
        characters = characters.OrderByDescending(x => x.GetComponent<Entity>().agility).ToList();
        characterSelected = characters.Count - 1;
        NextPlayer();
    }
    public void NextPlayer()
    {
        switch (characters[characterSelected].tag)
        {
            case "Player":
                switch (characters[(characterSelected + 1) % characters.Count].tag)
                {
                    case "Player":
                        if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
                        break;
                    case "Enemy":
                        if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
                        PlayerPanel.instance.gameObject.SetActive(false);
                        break;
                }
                break;
            case "Enemy":
                switch (characters[(characterSelected + 1) % characters.Count].tag)
                {
                    case "Player":
                        if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
                        PlayerPanel.instance.gameObject.SetActive(true);
                        break;
                    case "Enemy":
                        if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
                        break;
                }
                break;
        }
        TurnPanel.instance.SetTurns();
    }
    public GameObject SelectedPlayer(int addPosition = 0)
    {
        return characters[(characterSelected + addPosition) % characters.Count()];
    }
    public void NextTurn()
    {
        SelectedPlayer().GetComponent<Entity>().EndTurn();
        if(characterSelected != 0)
        {
            return;
        }
        GridCreator.instance.GenerateSeed();
    }
    public void Died(GameObject entity)
    {
        if (entity.tag == "Enemy")
        {
            enemys.Remove(entity);
            switch (Random.Range(0, 6))
            {
                case 0:
                    teamManager.AddItemAbdomen();
                    break;
                case 1:
                    teamManager.AddItemShell();
                    break;
                case 2:
                    teamManager.AddItemLeg();
                    break;
                case 3:
                    teamManager.AddItemHorn();
                    break;
                case 4:
                    teamManager.AddItemWing();
                    break;
                case 5:
                    teamManager.AddItemChest();
                    break;
            }
            if (enemys.Count == 0)
            {
                Finish();
            }
        }
        else if (entity.tag == "Player")
        {
            //int j = 0;
            //for (int i = 0; j < teamManager.GetPlantsList().Count; i++, j++) {
            //    //Debug.Log(players[i] == entity);
            //    while (playerLivePoints[j] != 0 && j < playerLivePoints.Count)
            //        j++;
            //    if (players[i] == entity)
            //    {
            //        playerLivePoints[j] = -1;
            //        break;
            //    }
            //}
            playerLivePoints.Add(0);
            players.Remove(entity);
            if (players.Count == 0)
            {
                Finish();
            }
        }
        Destroy(entity);
        if(characterSelected > characters.IndexOf(entity)) { characterSelected -= 1; }
        characters.Remove(entity);
    }
    public void Finish()
    {
        //int j = 0;
        //for (int i = 0; j < playerLivePoints.Count; i++, j++) 
        //{
        //    while (playerLivePoints[j] != 0)
        //    {
        //        j++;
        //        if (j == playerLivePoints.Count)
        //            break;
        //    }
        //    if (j < playerLivePoints.Count)
        //    {
        //        playerLivePoints[j] = players[i].GetComponent<Plants>().livePoints;
        //        //break;
        //    }
        //}
        for (int i = 0; i < players.Count; i++)
        {
            playerLivePoints.Add(players[i].GetComponent<Plants>().livePoints);
        }
        for(int i = 0; i < playerLivePoints.Count; i++)
        {
            teamManager.GetPlantsList()[i].SetCurrentHP(playerLivePoints[i]);
        }
        
        if (enemys.Count == 0)
        {
            defaultWinButton.SetActive(false);
            winScreen.SetActive(true);
        }
        else if(players.Count == 0)
        {
            defaultWinButton.SetActive(false);
            looseScreen.SetActive(true);
        }
    }
    public void AddWater()
    {
        teamManager.AddWater(1);
    }
}
