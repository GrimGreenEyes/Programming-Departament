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
    private int gridX, gridY;

    private void Start()
    {
        MovementPoint = transform.position;
    }
    public void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (moveing)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovementPoint, MovementSpeed * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, MovementPoint) == 0)
            {
                moveing = false;
            }
        }

        if ((input.x != 0 ^ input.y != 0) && !moveing)
        {
            angle = (input.y == 0)? new Vector3(0, 0, Mathf.Atan(tileScale.y / tileScale.x) * Mathf.Rad2Deg): new Vector3(0, 0, Mathf.Atan(tileScale.x / tileScale.y) * Mathf.Rad2Deg);
            Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + new Vector2((Quaternion.Euler(angle)* input).x, (Quaternion.Euler(angle) * input).y);

            if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            {
                moveing = true;
                MovementPoint += new Vector3((Quaternion.Euler(angle) * input).x, (Quaternion.Euler(angle) * input).y, 0);
            }
        }
    }
    public void SetPosition(Tile tile)
    {
        gameObject.transform.position = tile.transform.position + new Vector3(0, 0.25f, 0);
        MovementPoint = gameObject.transform.position;
        
    }
}
