using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Plants : Entity
{


    public void SetAttack(Attack newAttack)
    {
        mainAttack = newAttack;
    }
    public void SetSkill(Skill newSkill, int position)
    {
        skills[position] = newSkill;
    }
    private void OnMouseDown()
    {
        Debug.Log("mouseDown");
        if (UIHoverListener.instance.isUIOverride)
        {
            return;
        }
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == EntityState.USINGSKILL)
        {
            Debug.Log("Skill use");
            if (GameController.instance.SelectedPlayer() == gameObject && !GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[skillSelected].isReflexive)
            {
                Debug.LogError("Skill return");
                return;
            }
            Debug.Log(GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing);
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing)
            {
                Debug.Log("skill buff");
                GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
            }
        }
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == EntityState.IDLE)
        {
            if (Mathf.Abs(gridX - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX) + Mathf.Abs(gridY - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY) > GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.range)
            {
                return;
            }
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().attacked)
            {
                return;
            }
            if (!GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.directedToAlly)
            {
                return;
            }
            GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
            GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.ATTACKING;
        }
    }
    public void selectSkill(int position)
    {
        if (skillSelected == position)
        {
            actualState = EntityState.IDLE;
            skillSelected = skills.Count;
            return;
        }
        skillSelected = position;
        actualState = EntityState.USINGSKILL;
    }
    private void Update()
    {
        States();
        livePointsText.text = livePoints.ToString() + "/" + maxLivePoints.ToString();
    }
    public override void States()
    {
        switch (actualState)
        {
            case EntityState.START:
                
                if (hidden) Show(); 
                movement = maxMovement;
                attacked = false;
                if(bleeding) livePoints -= 5;
                
                if(livePoints <= 0)
                {
                    actualState = EntityState.IDLE;
                    GameController.instance.NextPlayer();
                    GameController.instance.Died(gameObject);
                }
                for (int i = 0; i < skills.Count; i++)
                {
                    skills[i].ReduceCoolDown();
                }
                if (gameObject == GameController.instance.SelectedPlayer())
                {

                    PlayerPanel.instance.ChangePlayer(gameObject);
                }
                actualState = EntityState.IDLE;
                break;
            case EntityState.IDLE:
                if (gameObject == GameController.instance.SelectedPlayer())
                {
                    PathFinding.instance.PathShine(thisTile);
                }
                MovementPoint = transform.position;
                moveing = false;
                path = null;
                break;
            case EntityState.MOVEING:
                if (path == null)
                {
                    path = PathFinding.instance.PathFind(thisTile, destination);
                    pathPosition = path.Count() - 1;
                }
                Move();
                break;
            case EntityState.ATTACKING:
                Debug.Log("attacking");
                attacked = true;
                for (int i = 0; i < GetComponent<Entity>().skills.Count; i++)
                    if (GetComponent<Entity>().skills[i].isDoingDamage)
                        skills[i].Effect(mainObjective, gameObject);
                for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                {
                    if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                        mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                }
                mainAttack.Effect(mainObjective, gameObject);

                break;
            case EntityState.STUNED:
                
                break;
            case EntityState.USINGSKILL:
                //GridCreator.instance.ShineTiles(gridX, gridY, skills[skillSelected].radious, false);
                if (skills[skillSelected].actilveOnClick)
                {
                    skills[skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
                }
                else
                {
                    GridCreator.instance.SearchObjective(gridX, gridY, skills[skillSelected].radious, false);
                }
                break;
            case EntityState.FINISHED:
                
                break;
        }
    }
    private Color saveColor;
    public void Hide(Color hideColor) 
    {
        saveColor = renderer.color;
        hidden = true;
        renderer.color = hideColor;
        defenseMultiplayer = 2;
        actualState = EntityState.IDLE;
        GameController.instance.NextPlayer();
    }
    public void Show()
    {
        hidden = false;
        renderer.color = saveColor;
        defenseMultiplayer = 1;
    }
}
