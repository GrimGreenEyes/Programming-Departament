using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
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

    private void Start()
    {
        MovementPoint = transform.position;
    }
    public void Move()
    {
        //input.x = Input.GetAxisRaw("Horizontal");
        //input.y = Input.GetAxisRaw("Vertical");
        input.x = gridX - destination.GetComponent<Tile>().GetX();
        input.y = destination.GetComponent<Tile>().GetY() - gridY;
        direction = new Vector2(0, 0);
        if (moveing)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovementPoint, MovementSpeed * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, MovementPoint) == 0)
            {
                moveing = false;
                GetComponent<Plants>().actualState = Plants.PlantState.IDLE;
            }
        }
        if (input.x != 0)
        {
            direction = new Vector2(0, input.x);
        }
        else if(input.y != 0)
        {
            direction = new Vector2(input.y, 0);
        }

        if ((direction.x != 0 ^ direction.y != 0) && !moveing)
        {
            
            
            angle = (direction.y == 0)? new Vector3(0, 0, Mathf.Atan(tileScale.y / tileScale.x) * Mathf.Rad2Deg): new Vector3(0, 0, Mathf.Atan(tileScale.x / tileScale.y) * Mathf.Rad2Deg);
            Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + new Vector2((Quaternion.Euler(angle)* direction).x, (Quaternion.Euler(angle) * direction).y);

            if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            {
                moveing = true;
                GetComponent<Plants>().actualState = Plants.PlantState.MOVEING;
                MovementPoint += new Vector3((Quaternion.Euler(angle) * direction).x * offsetMovePoint.x, (Quaternion.Euler(angle) * direction).y * offsetMovePoint.y, 0);
            }
        }
        if (gridX == destination.GetComponent<Tile>().GetX() && gridY == destination.GetComponent<Tile>().GetY())
        {
            
        }
    }
    public void SetDestination(Tile tile)
    {
        //gameObject.transform.position = tile.transform.position + new Vector3(0, 0.25f, 0);
        //MovementPoint = gameObject.transform.position;
        destination = tile.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "tile")
        {
            gridX = collision.gameObject.transform.parent.GetComponent<Tile>().GetX();
            gridY = collision.gameObject.transform.parent.GetComponent<Tile>().GetY();
            
        }
    }
}
