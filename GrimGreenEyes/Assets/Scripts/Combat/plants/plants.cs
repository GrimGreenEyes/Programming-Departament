using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Plants : Entity
{

    public Sprite HUDSprite;
    public float HUDSpriteSize;


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
            skillSelected = skills.Length;
            return;
        }
        skillSelected = position;
        actualState = EntityState.USINGSKILL;
    }
    private void Update()
    {
        States();
    }
    public override void States()
    {
        switch (actualState)
        {
            case EntityState.START:
                
                if (hidden) { Show(); }
                movement = maxMovement;
                if(bleeding) livePoints -= (maxLivePoints * 5/100); 
                if(livePoints <= 0)
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
                if (gameObject == GameController.instance.SelectedPlayer())
                {
                    PathFinding.instance.PathShine(thisTile);
                    //GridCreator.instance.ShineTiles(gridX, gridY, movement, true);
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

    ///
    ///MOVEMENT
    ///

    private void Start()
    {
        MovementPoint = transform.position;
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            skillObjects[i] = GameObject.Instantiate(skillPrefabs[i], gameObject.transform);
            skills[i] = skillObjects[i].GetComponent<Skill>();
        }
    }
    public void Move()
    {
        if(path.Count() == 0)
        {
            GetComponent<Plants>().actualState = Plants.EntityState.IDLE;
            return;
        }
        //input.x = gridX - destination.GetComponent<Tile>().GetX();
        //input.y = destination.GetComponent<Tile>().GetY() - gridY;
        //direction = new Vector2(0, 0);
        if (moveing)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovementPoint, MovementSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, MovementPoint) == 0)
            {
                movement--;
                moveing = false;
                pathPosition = (pathPosition <= 0)? 0 : pathPosition - 1;
            }
        }
        directionX = path[pathPosition].GetComponent<Tile>().GetY() - gridY;
        directionY = gridX - path[pathPosition].GetComponent<Tile>().GetX();
        if (directionX != 0)
        {
            direction = new Vector2(directionX, 0);
        }
        else if(directionY != 0)
        {
            direction = new Vector2(0, directionY);
        }
        //if (input.x != 0)
        //{
        //    direction = new Vector2(0, (input.x < 0) ? -1 : 1);
        //}
        //else if (input.y != 0)
        //{
        //    direction = new Vector2((input.y < 0) ? -1 : 1, 0);
        //}

        if ((direction.x != 0 ^ direction.y != 0) && !moveing)
        {

            angle = (direction.x == 0) ? new Vector3(0, 0, Mathf.Atan(tileScale.x / tileScale.y) * Mathf.Rad2Deg) : new Vector3(0, 0, Mathf.Atan(tileScale.y / tileScale.x) * Mathf.Rad2Deg);
            //Debug.Log(angle);
            Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + new Vector2((Quaternion.Euler(angle) * direction).x, (Quaternion.Euler(angle) * direction).y);

            if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            {
                moveing = true;
                MovementPoint += new Vector3((Quaternion.Euler(angle) * direction).x * offsetMovePoint.x, (Quaternion.Euler(angle) * direction).y * offsetMovePoint.y, 0);
            }
        }
        if (transform.position == destination.GetComponent<Tile>().transform.position + new Vector3(0, 0.25f, 0) || movement == 0)
        {
            GetComponent<Plants>().actualState = Plants.EntityState.IDLE;
            //path.Clear();
        }
    }
    public void SetDestination(Tile tile)
    {
        GetComponent<Plants>().actualState = Plants.EntityState.MOVEING;
        destination = tile.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "tile":
                gridX = collision.gameObject.GetComponent<Tile>().GetX();
                gridY = collision.gameObject.GetComponent<Tile>().GetY();
                thisTile = collision.gameObject.gameObject;
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
