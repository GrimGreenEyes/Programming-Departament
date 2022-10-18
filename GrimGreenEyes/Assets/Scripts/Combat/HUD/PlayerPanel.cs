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
        Plants plant = newPlant.GetComponent<Plants>();
        image.sprite = plant.HUDSprite;
        nameText.SetText(plant.name);
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
                button.GetComponent<ButtonManager>().Init(i);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = plant.skills[i].name;
                x++;
            }
        }
    }
    
}
