using System.Collections;
using System.Collections.Generic;
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
                //movement = maxMovement;
                //if (bleeding) livePoints -= (maxLivePoints * 5 / 100);
                actualState = EntityState.IDLE;
                break;
            case EntityState.IDLE:
                //if (gameObject == GameController.instance.SelectedPlayer())
                //{
                //    PathFinding.instance.PathShine(thisTile);
                //    //GridCreator.instance.ShineTiles(gridX, gridY, movement, true);
                //}
                //MovementPoint = transform.position;
                //moveing = false;
                //path = null;
                break;
            case EntityState.MOVEING:
                //if (path == null)
                //{
                //    path = PathFinding.instance.PathFind(thisTile, destination);
                //    pathPosition = path.Count() - 1;
                //}
                //Move();
                break;
            case EntityState.ATTACKING:
                Debug.Log("attacking");
                mainAttack.Effect(mainObjective, gameObject);
                break;
            case EntityState.STUNED:

                break;
            case EntityState.USINGSKILL:
                //GridCreator.instance.ShineTiles(gridX, gridY, skills[skillSelected].radious, false);
                GridCreator.instance.SearchObjective(gridX, gridY, skills[skillSelected].radious, false);

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
