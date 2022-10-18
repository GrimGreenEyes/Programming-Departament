using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Plants : Entity
{

    public Sprite HUDSprite;

    public enum PlantState {START, IDLE, MOVEING, ATTACKING, STUNED, USINGSKILL}
    public PlantState actualState = PlantState.IDLE;

    [Header("Combat")]
    public Attack mainAttack;
    public Skill[] skills = new Skill[3];
    public int skillSelected = 3;

    public GameObject mainObjective;
    

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
        if(actualState == PlantState.USINGSKILL)
        {
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[skillSelected].isbuffing)
            {
                GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[skillSelected].Effect();
            }
        }
    }
    public void selectSkill(int position)
    {
        if (skillSelected == position)
        {
            actualState = PlantState.IDLE;
            skillSelected = skills.Length;
            return;
        }
        skillSelected = position;
        actualState = PlantState.USINGSKILL;
    }
    private void Update()
    {
        States();
    }
    private void States()
    {
        switch (actualState)
        {
            case PlantState.START:
                movement = MAX_MOVEMENT;
                actualState = PlantState.IDLE;
                break;
            case PlantState.IDLE:
                if (gameObject == GameController.instance.SelectedPlayer())
                {
                    PathFinding.instance.PathShine(thisTile);
                    //GridCreator.instance.ShineTiles(gridX, gridY, movement, true);
                }
                MovementPoint = transform.position;
                moveing = false;
                path = null;
                break;
            case PlantState.MOVEING:
                if (path == null)
                {
                    Debug.Log("I am moveing");
                    path = PathFinding.instance.PathFind(thisTile, destination);
                    pathPosition = path.Count() - 1;
                }
                Move();
                break;
            case PlantState.ATTACKING:
                mainAttack.Effect(mainObjective, gameObject);
                break;
            case PlantState.STUNED:
                
                break;
            case PlantState.USINGSKILL:
                GridCreator.instance.ShineTiles(gridX, gridY, skills[skillSelected].radious, false);
                break;
        }
    }

    ///
    ///MOVEMENT
    ///

    private void Start()
    {
        MovementPoint = transform.position;
    }
    public void Move()
    {
        if(path.Count() == 0)
        {
            GetComponent<Plants>().actualState = Plants.PlantState.IDLE;
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
            Debug.Log(direction);

            angle = (direction.x == 0) ? new Vector3(0, 0, Mathf.Atan(tileScale.x / tileScale.y) * Mathf.Rad2Deg) : new Vector3(0, 0, Mathf.Atan(tileScale.y / tileScale.x) * Mathf.Rad2Deg);
            //Debug.Log(angle);
            Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + new Vector2((Quaternion.Euler(angle) * direction).x, (Quaternion.Euler(angle) * direction).y);

            if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            {
                moveing = true;
                //Debug.Log(MovementPoint);
                MovementPoint += new Vector3((Quaternion.Euler(angle) * direction).x * offsetMovePoint.x, (Quaternion.Euler(angle) * direction).y * offsetMovePoint.y, 0);
                Debug.Log(MovementPoint);
                Debug.LogWarning(path[pathPosition]);
            }
        }
        if (transform.position == destination.GetComponent<Tile>().transform.position + new Vector3(0, 0.25f, 0) || movement == 0)
        {
            GetComponent<Plants>().actualState = Plants.PlantState.IDLE;
            //path.Clear();
        }
    }
    public void SetDestination(Tile tile)
    {
        GetComponent<Plants>().actualState = Plants.PlantState.MOVEING;
        Debug.Log("moveing");
        destination = tile.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "tile":
                gridX = collision.gameObject.transform.parent.GetComponent<Tile>().GetX();
                gridY = collision.gameObject.transform.parent.GetComponent<Tile>().GetY();
                thisTile = collision.gameObject.transform.parent.gameObject;
                break;
        }
    }
}
