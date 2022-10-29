using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private int characterSelected = -1;
    private List<GameObject> characters = new List<GameObject>();
    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> enemys = new List<GameObject>();

    public GameObject winScreen;
    public GameObject looseScreen;

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
        teamManager = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
    }

    public void Init(GameObject entity)
    {
        characters.Add(entity);
        if(entity.tag == "Player")
        {
            players.Add(entity);
        }
        else if(entity.tag == "Enemy")
        {
            enemys.Add(entity);
        }
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
                        if (characters[characterSelected].GetComponent<Plants>().actualState != Entity.EntityState.IDLE)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Plants>().actualState = Entity.EntityState.START;
                        break;
                    case "Enemy":
                        if (characters[characterSelected].GetComponent<Plants>().actualState != Entity.EntityState.IDLE)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Mosquitoes>().actualState = Entity.EntityState.START;
                        PlayerPanel.instance.gameObject.SetActive(false);
                        break;
                }
                break;
            case "Enemy":
                switch (characters[(characterSelected + 1) % characters.Count].tag)
                {
                    case "Player":
                        if (characters[characterSelected].GetComponent<Mosquitoes>().actualState != Entity.EntityState.IDLE)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Plants>().actualState = Entity.EntityState.START;
                        PlayerPanel.instance.gameObject.SetActive(true);
                        break;
                    case "Enemy":
                        if (characters[characterSelected].GetComponent<Mosquitoes>().actualState != Entity.EntityState.IDLE)
                        {
                            return;
                        }
                        characterSelected = (characterSelected + 1) % characters.Count;
                        characters[characterSelected].GetComponent<Mosquitoes>().actualState = Entity.EntityState.START;
                        break;
                }
                break;
        }
        if(characterSelected == 0)
        {
            NewTurn();
        }
    }
    public GameObject SelectedPlayer()
    {
        return characters[characterSelected];
    }
    private void NewTurn()
    {
        GridCreator.instance.GenerateSeed();
    }
    public void Died(GameObject entity)
    {
        Destroy(entity);
        if (entity.tag == "Enemy")
        {
            enemys.Remove(entity);
            switch(Random.Range(0, 6))
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
        else if(entity.tag == "Player")
        {
            players.Remove(entity);
            if (players.Count == 0)
            {
                Finish();
            }
        }
        if(characterSelected > characters.IndexOf(entity)) { characterSelected -= 1; }
        characters.Remove(entity);
    }
    public void Finish()
    {
        teamManager.AddWater(3);

        if (enemys.Count == 0)
        {
            winScreen.SetActive(true);
        }
        else if(players.Count == 0)
        {
            looseScreen.SetActive(true);
        }
    }
}
