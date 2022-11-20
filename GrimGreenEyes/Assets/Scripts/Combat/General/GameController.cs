using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public int characterSelected = -1;
    public int turn;
    private List<GameObject> characters = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private List<int> playerLivePoints = new List<int>();
    public List<GameObject> enemys = new List<GameObject>();
    private GameObject carriage;

    public GameObject winScreen;
    public GameObject looseScreen;
    public GameObject defaultWinButton;

    [SerializeField] private GameObject nextTurnButton;

    private TeamInfo teamManager;

    public GameObject playerTurn;
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
        teamManager = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
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
        else if(entity.tag == "Carriage")
        {
            carriage = entity;
        }
    }
    public int CharacterCount()
    {
        return characters.Count();
    }
    public void AddEnemy(GameObject newEnemyPref, Transform position)
    {
        GameObject newEnemy = Instantiate(newEnemyPref, position);
        Init(newEnemy);
    }
    public void OrderCharacters()
    {
        characters = characters.OrderByDescending(x => x.GetComponent<Entity>().agility).ToList();
        characterSelected = 0;
        characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        PlayerPanel.instance.ChangePlayer(characters[characterSelected]);
    }
    public void NextPlayer()
    {
        playerTurn = characters[characterSelected];
        if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        {
            return;
        }
        characterSelected = (characterSelected + 1) % characters.Count;
        characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        nextTurnButton.SetActive(true);
        if (characters[characterSelected].tag == "Enemy")
        {
            nextTurnButton.SetActive(false);
        }

        turn++;
        //switch (characters[characterSelected].tag)
        //{
        //    case "Player":
        //        switch (characters[(characterSelected + 1) % characters.Count].tag)
        //        {
        //            case "Player":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                break;
        //            case "Enemy":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                PlayerPanel.instance.gameObject.SetActive(false);
        //                break;
        //            case "Carriage":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                break;
        //        }
        //        break;
        //    case "Enemy":
        //        switch (characters[(characterSelected + 1) % characters.Count].tag)
        //        {
        //            case "Player":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                PlayerPanel.instance.gameObject.SetActive(true);
        //                break;
        //            case "Enemy":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                if (PlayerPanel.instance.gameObject.activeSelf)
        //                {
        //                    PlayerPanel.instance.gameObject.SetActive(false);
        //                }
        //                break;
        //            case "Carriage":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                PlayerPanel.instance.gameObject.SetActive(true);
        //                break;
        //        }
        //        break;
        //    case "Carriage": 
        //        switch(characters[(characterSelected + 1) % characters.Count].tag)
        //        {
        //            case "Player":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                break;
        //            case "Enemy":
        //                if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED)
        //                {
        //                    return;
        //                }
        //                characterSelected = (characterSelected + 1) % characters.Count;
        //                characters[characterSelected].GetComponent<Entity>().actualState = Entity.EntityState.START;
        //                PlayerPanel.instance.gameObject.SetActive(false);
        //                break;
        //        }
        //        break;
        //}
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
            
        }
        Destroy(entity);
        if(characterSelected > characters.IndexOf(entity)) { characterSelected -= 1; }
        characters.Remove(entity);
    }
    public void Finish(bool victory)
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
            //teamManager.GetPlantsList()[i].SetCurrentHP(playerLivePoints[i]);
        }
        if (victory)
        {
            winScreen.SetActive(true);
        }       
        else
        {
            looseScreen.SetActive(true);
        }
    }

    public void AddWater(int water)
    {
        teamManager.AddWater(water);
    }
}
