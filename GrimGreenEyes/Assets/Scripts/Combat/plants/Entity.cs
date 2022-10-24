using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public float attackMultiplayer;
    public float defenseMultiplayer;
    public float heatResistanceMultiplayer;
    public float freezeResistanceMultiplayer;

    public bool bleeding = false;
    public bool hidden = false;
    public bool attacked = false;

    

    public Sprite timeLineSprite;
    public SpriteRenderer renderer;


    public enum EntityState { START, IDLE, MOVEING, ATTACKING, STUNED, USINGSKILL, FINISHED }
    public EntityState actualState = EntityState.IDLE;


    [Header("Combat")]
    public Attack mainAttack;
    [SerializeField] protected GameObject[] skillPrefabs = new GameObject[3];
    [SerializeField] protected GameObject[] skillObjects = new GameObject[3];
    public Skill[] skills = new Skill[3];
    public int skillSelected = 3;

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

    private void Awake()
    {
        
        renderer = GetComponent<SpriteRenderer>();
    }
    public virtual void States() { }
}
