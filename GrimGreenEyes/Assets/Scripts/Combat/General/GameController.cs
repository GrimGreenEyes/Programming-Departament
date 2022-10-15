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
    }
    public void NextPlayer()
    {
        if (characters[characterSelected].GetComponent<Plants>().actualState != Plants.PlantState.IDLE)
        {
            return;
        }
        characterSelected = (characterSelected + 1) % (characters.Length - 1);
        characters[characterSelected].GetComponent<Plants>().actualState = Plants.PlantState.START;
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
