using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquitoes : Entity
{
    private void OnMouseDown()
    {
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Plants.PlantState.USINGSKILL)
        {
            GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].Effect();
        }
        else
        {
            GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
            GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Plants.PlantState.ATTACKING;
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
                gridX = collision.gameObject.transform.parent.GetComponent<Tile>().GetX();
                gridY = collision.gameObject.transform.parent.GetComponent<Tile>().GetY();
                thisTile = collision.gameObject.transform.parent.gameObject;
                break;
        }
    }
}
