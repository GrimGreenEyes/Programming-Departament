using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public new string name;
    public string description;
    public bool isActiveSkill;
    public bool isbuffing;
    public bool isReflexive;
    public bool actilveOnClick;
    public int radious;
    public int areaOfEffect;

    public bool alreadyUsed;
    public int coolDown;
    public int currentCoolDown;
    
    
    
    public void ReduceCoolDown()
    {
        currentCoolDown = (currentCoolDown <= 0) ? 0 : currentCoolDown - 1 ;
        if (currentCoolDown == 0)
        {
            alreadyUsed = false;
        }
    }
    public void DeactivateSkill(Plants player)
    {
        PlayerPanel.instance.SetButtonsToInactive();
        alreadyUsed = true;
        currentCoolDown = coolDown;
        player.skillSelected = player.skills.Length;
        player.actualState = Entity.EntityState.IDLE;
    }
    public virtual void Effect(GameObject enemy, GameObject player) 
    {
        Debug.Log("then, does this");
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
