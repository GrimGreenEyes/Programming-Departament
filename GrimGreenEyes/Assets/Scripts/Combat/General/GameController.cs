using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public int characterSelected = -1;
    public int turn;
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> allPlayers = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private List<int> playerLivePoints = new List<int>();
    public List<GameObject> enemys = new List<GameObject>();
    private GameObject carriage;

    
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private GameObject defaultWinButton;
    private Dictionary<string, int> obtainedResources = new Dictionary<string, int>();

    [SerializeField] private GameObject nextTurnButton;
    [SerializeField] private TextMeshProUGUI victoryResourcesText;

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
            allPlayers.Add(entity);
            playerLivePoints.Add(0);
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
        newEnemy.transform.parent = null;
        newEnemy.transform.position = position.position;
        Debug.Log(position);

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
        if (characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.FINISHED && characters[characterSelected].GetComponent<Entity>().actualState != Entity.EntityState.WAITING)
        {
            return;
        }
        if(playerTurn.tag == "Carriage" && playerTurn.GetComponent<Entity>().thisTile.GetComponent<Tile>().addsWater)
        {
            AddWater(2);
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
        
    }
    public void Died(GameObject entity)
    {
        if (entity.tag == "Enemy")
        {
            enemys.Remove(entity);
            switch (Random.Range(1, 6))
            {
                case 1:
                    GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(entity.GetComponent<Entity>(), "+1 Abdomen, +1 Caparaz??n", Color.cyan, 2.3f);
                    GameObject.Find("GameController").GetComponent<GameController>().AddResource("Caparaz??n de insecto");
                    teamManager.AddItemShell();
                    break;
                case 2:
                    GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(entity.GetComponent<Entity>(), "+1 Abdomen, +1 Ala", Color.cyan, 2.3f);
                    GameObject.Find("GameController").GetComponent<GameController>().AddResource("Ala de insecto");
                    teamManager.AddItemWing();
                    break;
                case 3:
                    GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(entity.GetComponent<Entity>(), "+1 Abdomen, +1 Cuerno", Color.cyan, 2.3f);
                    GameObject.Find("GameController").GetComponent<GameController>().AddResource("Cuerno de insecto");
                    teamManager.AddItemHorn();
                    break;
                case 4:
                    GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(entity.GetComponent<Entity>(), "+1 Abdomen, +1 Ala", Color.cyan, 2.3f);
                    GameObject.Find("GameController").GetComponent<GameController>().AddResource("Ala de insecto");
                    teamManager.AddItemWing();
                    break;
                case 5:
                    GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(entity.GetComponent<Entity>(), "+1 Abdomen, +1 T??rax", Color.cyan, 2.3f);
                    GameObject.Find("GameController").GetComponent<GameController>().AddResource("T??rax de insecto");
                    teamManager.AddItemChest();
                    break;
            }
            teamManager.AddItemAbdomen();
            GameObject.Find("GameController").GetComponent<GameController>().AddResource("Abdomen de insecto");
            if (enemys.Count == 0) { Finish(true); }
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
            players.Remove(entity);
            playerLivePoints[allPlayers.IndexOf(entity)] = 0;

        }
        else if (entity.tag == "Carriage")
        {
            Finish(false);
        }
        Destroy(entity);
        if(characterSelected > characters.IndexOf(entity)) { characterSelected -= 1; }
        characters.Remove(entity);
    }

    public void AddResource(string resource) //A??ade un item al diccionario (inventario). Si ese item ya exist??a, aumenta su cantidad en 1 unidad.
    {
        if (obtainedResources.ContainsKey(resource))
        {
            obtainedResources[resource] = obtainedResources[resource] + 1;
        }
        else
        {
            obtainedResources.Add(resource, 1);
        }
    }

    public void ShowResources()
    {
        string text = "<b>En la batalla\nhas conseguido:</b>";
        foreach (KeyValuePair<string, int> entry in obtainedResources)
        {
            text += "\n+ " + entry.Value + " " + entry.Key;
        }
        victoryResourcesText.text = text;
    }

    public void Finish(bool victory)
    {
        UI.SetActive(false);
        if (!victory)
        {
            looseScreen.SetActive(true);
            looseScreen.GetComponent<AudioSource>().Play();
            return;
        }
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
            playerLivePoints[allPlayers.IndexOf(players[i])] = players[i].GetComponent<Plants>().livePoints;
        }
        for(int i = 0; i < playerLivePoints.Count; i++)
        {
            teamManager.GetPlantsList()[i].SetCurrentHP(playerLivePoints[i]);
        }
        winScreen.SetActive(true);
        ShowResources();
        winScreen.GetComponent<AudioSource>().Play();
               
        
    }

    public void AddWater(int water)
    {
        teamManager.AddWater(water);
        GameObject.Find("StatusMsgManager").GetComponent<StatusMsgManager>().ShowMsg(carriage.GetComponent<Entity>(), "+2 Agua", Color.blue, 2f);
        GameObject.Find("GameController").GetComponent<GameController>().AddResource("Agua");
        GameObject.Find("GameController").GetComponent<GameController>().AddResource("Agua");
    }
}
