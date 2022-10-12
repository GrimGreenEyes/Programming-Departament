using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPanel : MonoBehaviour
{
    public static PlayerPanel instance;

    [SerializeField] private Sprite[] plants;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text livePointsText;
    [SerializeField] private TMP_Text nameText;


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
        image.sprite = newPlant.GetComponent<Plants>().HUDSprite;
        nameText.SetText(newPlant.GetComponent<Plants>().name);
        livePointsText.SetText(newPlant.GetComponent<Plants>().livePoints.ToString() + " / " + newPlant.GetComponent<Plants>().maxLivePoints.ToString());
    }

}
