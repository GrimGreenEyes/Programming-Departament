using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public new string name;
    public string description;
    public bool isActiveSkill;
    public bool isAttacking;
    public bool isbuffing;
    public bool isReciveingDamage;
    public bool isDoingDamage;
    public bool isReflexive;
    public bool actilveOnClick;
    public bool selectsStightTile;
    public bool selectsTile;
    public bool isOnSeed;
    public bool isOnDestination;
    public GameObject destinationTile;
    public int range;
    public int areaOfEffect;
    public int turnsActive;
    public int maxTurnsActive;

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
    public void CheckDead(Entity enemy)
    {
        if (enemy.livePoints <= 0)
        {
            GameController.instance.Died(enemy.gameObject);
        }
    }
    public void DeactivateSkill(Entity player)
    {
        PlayerPanel.instance.SetButtonsToInactive();
        alreadyUsed = true;
        currentCoolDown = coolDown;
        player.skillSelected = player.skills.Count;
        player.actualState = Entity.EntityState.IDLE;
    }
    public int DamageCalculator(Entity enemy, Entity player)
    {

        float totalDefense = enemy.defense +
            (
            enemy.heatResistance * player.mainAttack.heatDamage
            ) + (
            enemy.freezeResistance * player.mainAttack.freezeDamage
            );
        float damage = (player.attack) * totalDefense / 100f;
        return (int)damage;
    }
    public virtual void Effect(GameObject enemy, GameObject player) 
    {
        player.GetComponent<Plants>().actualState = Entity.EntityState.IDLE;
    }
}
