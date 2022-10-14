using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plants : Entity
{

    public Sprite HUDSprite;

    public enum PlantState { IDLE, MOVEING, ATTACKING, STUNED, USINGSKILL, FINISHED}
    public PlantState actualState = PlantState.IDLE;

    [Header("Combat")]
    public Skill[] skills = new Skill[3];
    public int skillSelected = 3;
    
    ///
    ///MOVEMENT
    ///

    [Header("Movement")]
    [SerializeField] private float MovementSpeed;
    [SerializeField] private Vector3 MovementPoint;
    [SerializeField] private Vector2 tileScale;
    [SerializeField] private Vector3 angle;
    [SerializeField] private Vector2 offsetMovePoint;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float radious;
    public bool moveing = false;
    private Vector2 input;
    private Vector2 direction;
    public int gridX, gridY;

    private GameObject destination;




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
        if (GameController.instance.SelectedPlayer() == this.gameObject)
        {
            Debug.Log("Same Player");
            return;
        }
        GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = PlantState.ATTACKING;
        GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.Effect(this.gameObject, GameController.instance.SelectedPlayer());

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
            case PlantState.IDLE:
                if (gameObject == GameController.instance.SelectedPlayer())
                {
                    GridCreator.instance.ShineTiles(gridX, gridY, movement, true);
                }
                break;
            case PlantState.MOVEING:
                Move();
                break;
            case PlantState.ATTACKING:
                
                break;
            case PlantState.STUNED:
                
                break;
            case PlantState.FINISHED:

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
        input.x = gridX - destination.GetComponent<Tile>().GetX();
        input.y = destination.GetComponent<Tile>().GetY() - gridY;
        direction = new Vector2(0, 0);
        if (moveing)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovementPoint, MovementSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, MovementPoint) == 0)
            {
                moveing = false;
                
            }
        }
        if (input.x != 0)
        {
            direction = new Vector2(0, input.x);
        }
        else if (input.y != 0)
        {
            direction = new Vector2(input.y, 0);
        }

        if ((direction.x != 0 ^ direction.y != 0) && !moveing)
        {


            angle = (direction.y == 0) ? new Vector3(0, 0, Mathf.Atan(tileScale.y / tileScale.x) * Mathf.Rad2Deg) : new Vector3(0, 0, Mathf.Atan(tileScale.x / tileScale.y) * Mathf.Rad2Deg);
            Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + new Vector2((Quaternion.Euler(angle) * direction).x, (Quaternion.Euler(angle) * direction).y);

            if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            {
                moveing = true;
                MovementPoint += new Vector3((Quaternion.Euler(angle) * direction).x * offsetMovePoint.x, (Quaternion.Euler(angle) * direction).y * offsetMovePoint.y, 0);
            }
        }
        if (transform.position == destination.GetComponent<Tile>().transform.position + new Vector3(0, 0.25f, 0))
        {
            GetComponent<Plants>().actualState = Plants.PlantState.IDLE;

        }
    }
    public void SetDestination(Tile tile)
    {
        GetComponent<Plants>().actualState = Plants.PlantState.MOVEING;
        destination = tile.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "tile":
                gridX = collision.gameObject.transform.parent.GetComponent<Tile>().GetX();
                gridY = collision.gameObject.transform.parent.GetComponent<Tile>().GetY();
                break;
        }
    }
}
