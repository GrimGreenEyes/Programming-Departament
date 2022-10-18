using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public int skillPosition;

    public void Init(int pos)
    {
        skillPosition = pos;
    }
    public void SelectSkill()
    {
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState != Plants.PlantState.IDLE)
        {
            return;
        }
        GameController.instance.SelectedPlayer().GetComponent<Plants>().selectSkill(skillPosition);
    }

}
