using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mosquitoes : Entity
{

    private void Update()
    {
        States();
    }
    public override void States()
    {
        switch (actualState)
        {
            case EntityState.START:

                //if (hidden) { Show(); }
                movement = maxMovement;
                if (bleeding) livePoints -= (maxLivePoints * 5 / 100);
                if (livePoints <= 0)
                {
                    actualState = EntityState.IDLE;
                    GameController.instance.NextPlayer();
                    GameController.instance.Died(gameObject);
                }
                for (int i = 0; i < skills.Length; i++)
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
                if (gameObject != GameController.instance.SelectedPlayer())
                {
                    return;
                    //GridCreator.instance.ShineTiles(gridX, gridY, movement, true);
                }
                int positionToMoveX = Random.Range(-movement, movement);
                int positionToMoveY = Random.Range(-movement, movement);
                Debug.Log(positionToMoveX);
                Debug.Log(positionToMoveY);
                if(Mathf.Abs(positionToMoveX) + Mathf.Abs(positionToMoveY) > movement || Mathf.Abs(positionToMoveX) + Mathf.Abs(positionToMoveY) <= 0)
                {
                    break;
                }
                SetDestination(GridCreator.instance.GetTile((gridX + positionToMoveX < 0 || gridX + positionToMoveX > GridCreator.instance.width) ? gridX - positionToMoveX : gridX + positionToMoveX, (gridY + positionToMoveY < 0 || gridY + positionToMoveY > GridCreator.instance.height) ? gridY - positionToMoveY : gridY + positionToMoveY));
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

    private void OnMouseDown()
    {
        if (UIHoverListener.instance.isUIOverride)
        {
            return;
        }
        if(GameController.instance.SelectedPlayer().tag != "Player")
        {
            return;
        }
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.USINGSKILL)
        {
            Debug.Log("skill");
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing)
            {
                return;
            }
            if (Mathf.Abs(gridX - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX) + Mathf.Abs(gridY - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY) > GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].radious)
            {
                return;
            }
            if (GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[GameController.instance.SelectedPlayer().GetComponent<Entity>().skillSelected].alreadyUsed)
            {
                return;
            }
            Debug.Log("effect");

            GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
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
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.directedToAlly)
            {
                return;
            }
            Debug.Log("main obgective");
            GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
            GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.ATTACKING;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Proyectil":
                livePoints -= collision.gameObject.GetComponentInParent<Plants>().mainAttack.DamageCalculator(gameObject.GetComponent<Mosquitoes>(), collision.gameObject.GetComponentInParent<Plants>());
                print(livePoints);
                break;
            case "tile":
                gridX = collision.gameObject.GetComponent<Tile>().GetX();
                gridY = collision.gameObject.GetComponent<Tile>().GetY();
                thisTile = collision.gameObject.gameObject;
                break;
        }
    }
}
