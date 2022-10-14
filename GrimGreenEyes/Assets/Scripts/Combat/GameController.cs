using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private int characterSelected = 0;
    private int maxPlayer = 0;
    private GameObject[] characters = new GameObject[9];
    private int input;



    private void Awake()
    {
        if(instance == null)
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
    public void Init(GameObject player, int playerPos)
    {
        characters[playerPos] = player;
        maxPlayer++;
    }
    private void Update()
    {
        
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePlayer(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangePlayer(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangePlayer(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangePlayer(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangePlayer(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangePlayer(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangePlayer(8);
        }*/
    }
    /*private void ChangePlayer(int number)
    {
        input = (maxPlayer > number) ? number : input;
        characterSelected = input;
        PlayerPanel.instance.ChangePlayer(characters[characterSelected]);
    }*/
    public void NextPlayer()
    {
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState != Plants.PlantState.IDLE)
        {
            return;
        }
        Debug.Log(characterSelected);
        characterSelected = (characterSelected + 1) % (characters.Length - 1);
        PlayerPanel.instance.ChangePlayer(characters[characterSelected]);
    }
    public GameObject SelectedPlayer()
    {
        return characters[characterSelected];
    }
    /*
    public bool IsPointerOverUIObject(Touch touch)
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    */
}
