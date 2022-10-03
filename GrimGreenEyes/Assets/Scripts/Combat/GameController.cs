using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        characters[characterSelected].GetComponent<TileMovement>().Move();

        if (characters[characterSelected].GetComponent<TileMovement>().moveing)
        {
            return;
        }

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
        }
    }
    private void ChangePlayer(int number)
    {
        input = (maxPlayer > number) ? number : input;
        characterSelected = input;
    }

}
