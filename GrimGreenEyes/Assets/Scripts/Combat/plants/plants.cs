using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plants : MonoBehaviour
{
    [Header("Info")]
    public new string name;
    public Sprite HUDSprite;

    [Header("Stats")]
    public int livePoints;
    public int maxLivePoints;
    public int attack;
    public int defense;
    public int heatResistance;
    public int freezeResistance;
    public int agility;
    public const int movement = 5;

    
    [Header("Combat")]
    public Attack mainAttack;
    public Skill mainSkill;

    
    public void SetAttack(Attack newAttack)
    {
        mainAttack = newAttack;
    }
    public void SetSkill(Skill newSkill)
    {
        mainSkill = newSkill;
    }
    private void OnMouseDown()
    {
        if (GameController.instance.SelectedPlayer() == this.gameObject)
        {
            Debug.Log("Same Player");
            return;
        }
        GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.Effect(this.gameObject, GameController.instance.SelectedPlayer());
    }
    
}
