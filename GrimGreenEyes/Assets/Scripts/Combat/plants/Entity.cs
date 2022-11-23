using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour
{
    [Header("Info")]
    public new string name;

    [Header("Stats")]
    public int livePoints;
    public int maxLivePoints;
    public int attack;
    public int defense;
    public int heatResistance;
    public int freezeResistance;
    public int agility;
    public int movement;
    public int maxMovement;

    [SerializeField] protected List<int> att;
    [SerializeField] protected List<int> def;
    [SerializeField] protected List<int> ps;
    [SerializeField] protected List<int> mov;
    [SerializeField] protected List<int> agt;
    [SerializeField] protected List<int> hRes;
    [SerializeField] protected List<int> fRes;

    public float attackMultiplayer;
    public float defenseMultiplayer;
    public float heatResistanceMultiplayer;
    public float freezeResistanceMultiplayer;

    public bool bleeding = false;
    public bool poisoned = false;
    public bool stuned = false;
    public bool hidden = false;
    public bool attacked = false;


    protected GameObject startPanel;
    public Sprite timeLineSprite;
    public new SpriteRenderer renderer;
    private AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>(); //0 = perder vida ; 1 = 

    public enum EntityState { START, IDLE, MOVEING, ATTACKING, USINGSKILL, FINISHED , WAITING}
    public EntityState actualState = EntityState.IDLE;


    [Header("Combat")]
    public Attack mainAttack;
    [SerializeField] protected List<GameObject> skillPrefabs = new List<GameObject>();
    [SerializeField] protected List<GameObject> skillObjects = new List<GameObject>();
    public List<Skill> skills = new List<Skill>();
    public int skillSelected = 3;
    public int charges = 0;

    public GameObject mainObjective;

    ///
    ///Movement
    ///

    [Header("Movement")]
    [SerializeField] public float MovementSpeed;
    [SerializeField] public Vector3 MovementPoint;
    [SerializeField] public Vector2 tileScale;
    [SerializeField] public Vector3 angle;
    [SerializeField] public Vector2 offsetMovePoint;
    [SerializeField] public LayerMask obstacles;
    [SerializeField] public float radious;
    public bool moveing = false;
    public Vector2 input;
    public Vector2 direction;
    public int gridX, gridY;

    public GameObject thisTile;
    public GameObject destination;

    public List<GameObject> path = new List<GameObject>();
    public int pathPosition = 0;

    public float directionX;
    public float directionY;

    public TMP_Text livePointsText;

    public List<Transform> statusPositions;
    public GameObject statusImage;
    public int statusPosition;
    public List<GameObject> Status;
    private int boost1;
    private int boost2;
    private int boost3;
    private int boost4;
    private int boost5;


    public Sprite HUDSprite;
    public float HUDSpriteSize;
    public float HUDTurnSize;

    [Header("Animation")]
    private Animator animator;

    public bool isWalking;
    public bool moveAndAttack;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        startPanel = GameObject.Find("GlobalAttributes").transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
    }
    public void SetStats(int newLivePoints, int newMaxLivePoints, int newAttack, int newDefense, int newHeatResistance, int newFreezeResistance, int newAgility, int newMaxMovement)
    {
        this.livePoints = newLivePoints;
        this.maxLivePoints = newMaxLivePoints;
        this.attack = newAttack;
        this.defense = newDefense;
        this.heatResistance = newHeatResistance;
        this.freezeResistance = newFreezeResistance;
        this.agility = newAgility;
        this.movement = 0;
        this.maxMovement = newMaxMovement;
    }
    public void SetSkills(List<GameObject> skillList)
    {
        skillPrefabs = skillList;
    }
    public virtual void States() { }
    public void Start()
    {
        MovementPoint = transform.position;
        for (int i = 0; i < skillPrefabs.Count; i++)
        {
            skillObjects.Add(GameObject.Instantiate(skillPrefabs[i], gameObject.transform));
            skills.Add(skillObjects[i].GetComponent<Skill>());
        }
    }
    public void SetDestination(GameObject tile)
    {
        GetComponent<Entity>().actualState = Entity.EntityState.MOVEING;
        destination = tile;
    }
    public void Move()
    {
        if(audioClips.Count > 1 && !audioSource.isPlaying)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            audioSource.loop = true;
        }
        if (path.Count() == 0)
        {
            GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
            audioSource.Stop();
            audioSource.loop = false;
            return;
        }
        //input.x = gridX - destination.GetComponent<Tile>().GetX();
        //input.y = destination.GetComponent<Tile>().GetY() - gridY;
        //direction = new Vector2(0, 0);
        if (moveing)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovementPoint, MovementSpeed * Time.deltaTime);

            if (transform.position == MovementPoint)
            {
                movement--;

                moveing = false;
                pathPosition = (pathPosition <= 0) ? 0 : pathPosition - 1;
            }
        }
        directionX = path[pathPosition].GetComponent<Tile>().GetY() - gridY;
        directionY = gridX - path[pathPosition].GetComponent<Tile>().GetX();
        if (directionX != 0)
        {
            direction = new Vector2(directionX, 0);
        }
        else if (directionY != 0)
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

            //angle = (direction.x == 0) ? new Vector3(0, 0, Mathf.Atan(tileScale.x / tileScale.y) * Mathf.Rad2Deg) : new Vector3(0, 0, Mathf.Atan(tileScale.y / tileScale.x) * Mathf.Rad2Deg);
            //Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + new Vector2((Quaternion.Euler(angle) * direction).x, (Quaternion.Euler(angle) * direction).y);
            if(directionY == -1)
            {
                animator.SetInteger("direction", 0);
            }else if(directionY == 1){
                animator.SetInteger("direction", 1);
            }else if(directionX == -1)
            {
                animator.SetInteger("direction", 3);
            }else if(directionX == 1)
            {
                animator.SetInteger("direction", 2);
            }
            //if (!Physics2D.OverlapCircle(checkPoint, radious, obstacles))
            //{
            //    animator.SetBool("walking", true);
            //    moveing = true;
            //    MovementPoint += new Vector3((Quaternion.Euler(angle) * direction).x * offsetMovePoint.x, (Quaternion.Euler(angle) * direction).y * offsetMovePoint.y, 0);
            //}
            animator.SetBool("walking", true);
            MovementPoint = path[pathPosition].transform.position + new Vector3(0, 0.25f, 0);
            moveing = true;
            Debug.Log("IS WALKING!!");
        }
        if (transform.position == destination.transform.position + new Vector3(0, 0.25f, 0) && moveAndAttack)
        {
            animator.SetBool("walking", false);
            GetComponent<Entity>().actualState = Entity.EntityState.ATTACKING;
            audioSource.Stop();
            //path.Clear();
            //moveAndAttack = false;
            moveing = false;
            isWalking = false;
        }

        else if (transform.position == destination.transform.position + new Vector3(0, 0.25f, 0) || movement == 0)
        {
            animator.SetBool("walking", false);
            GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
            audioSource.Stop();
            //path.Clear();
            moveing = false;
            isWalking = false;
            Debug.Log("IS not :( WALKING!!");

        }
    }
    public void SetTile(GameObject tile)
    {
        if (tile.GetComponent<Tile>().isAcided)
        {
            int pathSize = path.Count();
            destination = tile;
            path.Clear();
            for (int i = 0; i < pathSize; i++)
            {
                path.Add(tile);
            }
            movement = 1;
        }
        gridX = tile.gameObject.GetComponent<Tile>().GetX();
        gridY = tile.gameObject.GetComponent<Tile>().GetY();
        Debug.Log(tile.gameObject.name + "  " + gameObject.name);
        thisTile = tile.gameObject.gameObject;
    }
    public void Heal(int quantity)
    {
        GetComponent<Entity>().livePoints = (GetComponent<Entity>().livePoints + quantity < GetComponent<Entity>().maxLivePoints) ? GetComponent<Entity>().livePoints + quantity : GetComponent<Entity>().maxLivePoints;
    }
    public void EndTurn()
    {
        if(actualState != EntityState.IDLE) { return; }
        actualState = EntityState.FINISHED;
    }
    public void CheckDead()
    {
        if (livePoints <= 0)
        {
            GameController.instance.Died(gameObject);
        }
    }
    public void Damage(int damage)
    {
        livePoints -= damage;
        audioSource.clip = audioClips[0];
        audioSource.Play();
        CheckDead();
    }
    public void Poison()
    {
        poisoned = true;
        //Status.Add(Instantiate(statusImage, statusPositions[statusPosition]));
        //Status.Last().
    }
    public void Stun()
    {
        stuned = true;
    }
    public void AttackBust(int num)
    {
        attack += num;
    }
    public void DefenseBust(int num)
    {
        defense += num;
        //Instantiate()
    }
}
