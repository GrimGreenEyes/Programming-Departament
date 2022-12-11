using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Color onButtonDownColor, onButtonUpColor;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text CD;
    public int skillPosition;
    public bool isActive;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void Init(int pos)
    {
        CD.text = GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[pos].currentCoolDown.ToString();
        if (!GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[pos].isActiveSkill)
        {
            CD.text = "P";
        }
        skillPosition = pos;
        if (!GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[pos].isActiveSkill)
        {

        }
        if (GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[pos].currentCoolDown > 0)
        {
            SetColor(onButtonDownColor);
        }
    }
    public void SelectSkill()
    {
        bool wasActive = isActive;
        PlayerPanel.instance.SetButtonsToInactive();
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.IDLE || GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.USINGSKILL)
        {
            if (!wasActive)
            {
                isActive = true;
                SetColor(onButtonDownColor);
                GameController.instance.SelectedPlayer().GetComponent<Plants>().selectSkill(skillPosition);
            }
        }
    }
    public void SetColor(Color color)
    {
        image.color = color;
    }
}
