using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{

    [SerializeField] private float MovementSpeed;
    [SerializeField] private Vector2 MovementPoint;
    [SerializeField] private Vector2 offsetMovePoint;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float radious;
    public bool moveing = false;
    private Vector2 input;

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
            transform.position = Vector2.MoveTowards(transform.position, MovementPoint, MovementSpeed * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, MovementPoint) == 0)
            {
                moveing = false;
            }
        }

        if ((input.x != 0 ^ input.y != 0) && !moveing)
        {
            Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint+ input;

            if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            {
                moveing = true;
                MovementPoint += input;
            }
        }
    }
}
