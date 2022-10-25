using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPanel : MonoBehaviour
{
    public static PlayerPanel instance;

    [SerializeField] private Sprite[] plants;
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text livePointsText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Transform[] butonPositions = new Transform[3];
    private List<GameObject> skillButtons = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnDestroy()
    {
        if(instance = this)
        {
            instance = null;
        }
    }
    public void ChangePlayer(GameObject newPlant)
    {
        Entity plant = newPlant.GetComponent<Entity>();
        image.sprite = plant.HUDSprite;
        image.type = Image.Type.Sliced;
        image.pixelsPerUnitMultiplier = plant.HUDSpriteSize;
        image.preserveAspect = true;
        nameText.SetText(plant.name);
        skillButtons.Clear();
        livePointsText.SetText(plant.livePoints.ToString() + " / " + plant.maxLivePoints.ToString());
        int x = 0;
        for (int i = 0; i < plant.skills.Length; i++)
        {
            if(plant.skills[i] == null)
            {
                return;
            }
            if (butonPositions[i].childCount > 0)
            {
                Destroy(butonPositions[i].GetChild(0).gameObject);
            }
            if (plant.skills[i].isActiveSkill)
            {
                GameObject button = Instantiate(buttonPref, butonPositions[x], false);
                skillButtons.Add(button);
                button.GetComponent<ButtonManager>().Init(i);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = plant.skills[i].name;
                x++;
            }
        }
    }
    public void SetButtonsToInactive()
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            skillButtons[i].GetComponent<ButtonManager>().isActive = false;
            skillButtons[i].GetComponent<ButtonManager>().SetColor(skillButtons[i].GetComponent<ButtonManager>().onButtonUpColor);
            GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
        }
    }

}
