using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Carriage : Entity
{
    private void Update()
    {
        if(livePoints <= 0)
        {
            GameController.instance.Finish(false);
        }
        if(gridY == GridCreator.instance.height - 1)
        {
            GameController.instance.Finish(true);
        }
        States();
        livePointsText.text = livePoints.ToString() + "/" + maxLivePoints.ToString();
    }
    public override void States()
    {
        switch (actualState)
        {
            case EntityState.START:
                movement = maxMovement;
                if (gameObject == GameController.instance.SelectedPlayer())
                {
                    PlayerPanel.instance.ChangePlayer(gameObject);
                }
                if (stuned)
                {
                    actualState = EntityState.FINISHED;
                }
                else
                {
                    actualState = EntityState.IDLE;
                }
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
            case EntityState.FINISHED:
                GameController.instance.NextPlayer();
                break;
        }
    }
}
