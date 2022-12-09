using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    public static TurnPanel instance;

    [SerializeField] private Transform turnPanel;
    [SerializeField] private Transform nextPanel;
    [SerializeField] private Transform secondNextPanel;
    [SerializeField] private Transform thirdNextPanel;
    [SerializeField] private Transform fourthNextPanel;
    [SerializeField] private GameObject thisPlantTurnPanel;
    [SerializeField] private GameObject thisEnemyTurnPanel;
    [SerializeField] private GameObject plantTurnPanel;
    [SerializeField] private GameObject enemyTurnPanel;
    private GameObject[] panels = new GameObject[5];

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
    public void SetTurns()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            Destroy(panels[i]);
        }
        panels[0] = Instantiate((GameController.instance.SelectedPlayer(0).tag == "Player") ? thisPlantTurnPanel : thisEnemyTurnPanel, turnPanel);
        panels[0].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer().GetComponent<Entity>().HUDSprite;
        panels[0].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[0].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer().GetComponent<Entity>().HUDTurnSize;
        
        panels[1] = Instantiate((GameController.instance.SelectedPlayer(1).tag == "Player") ? plantTurnPanel : enemyTurnPanel, nextPanel);
        panels[1].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer(1).GetComponent<Entity>().HUDSprite;
        panels[1].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[1].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer(1).GetComponent<Entity>().HUDTurnSize;
        
        panels[2] = Instantiate((GameController.instance.SelectedPlayer(2).tag == "Player") ? plantTurnPanel : enemyTurnPanel, secondNextPanel);
        panels[2].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer(2).GetComponent<Entity>().HUDSprite;
        panels[2].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[2].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer(2).GetComponent<Entity>().HUDTurnSize;

        panels[3] = Instantiate((GameController.instance.SelectedPlayer(3).tag == "Player") ? plantTurnPanel : enemyTurnPanel, thirdNextPanel);
        panels[3].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer(3).GetComponent<Entity>().HUDSprite;
        panels[3].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[3].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer(3).GetComponent<Entity>().HUDTurnSize;

        panels[4] = Instantiate((GameController.instance.SelectedPlayer(4).tag == "Player") ? plantTurnPanel : enemyTurnPanel, fourthNextPanel);
        panels[4].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer(4).GetComponent<Entity>().HUDSprite;
        panels[4].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[4].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer(4).GetComponent<Entity>().HUDTurnSize;

    }
}
