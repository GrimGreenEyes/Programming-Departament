using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    public static TurnPanel instance;

    [SerializeField] private Transform turnPanel;
    [SerializeField] private Transform nextPanel;
    [SerializeField] private Transform SecondNextPanel;
    [SerializeField] private GameObject thisTurnPanel;
    [SerializeField] private GameObject plantTurnPanel;
    [SerializeField] private GameObject enemyTurnPanel;
    private GameObject[] panels = new GameObject[3];

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
        for (int i = 0; i < panels.Length - 1; i++)
        {
            Destroy(panels[i]);
        }
        panels[0] = Instantiate(thisTurnPanel, turnPanel);
        panels[0].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer().GetComponent<Entity>().HUDSprite;
        panels[0].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[0].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer().GetComponent<Entity>().HUDSpriteSize;
        
        panels[1] = Instantiate((GameController.instance.SelectedPlayer(1).tag == "Player") ? plantTurnPanel : enemyTurnPanel, nextPanel);
        panels[1].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer(1).GetComponent<Entity>().HUDSprite;
        panels[1].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[1].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer(1).GetComponent<Entity>().HUDSpriteSize;
        
        panels[2] = Instantiate((GameController.instance.SelectedPlayer(2).tag == "Player") ? plantTurnPanel : enemyTurnPanel, SecondNextPanel);
        panels[2].transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.SelectedPlayer(2).GetComponent<Entity>().HUDSprite;
        panels[2].transform.GetChild(0).GetComponent<Image>().type = Image.Type.Tiled;
        panels[2].transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = GameController.instance.SelectedPlayer(2).GetComponent<Entity>().HUDSpriteSize;
    }
}
