using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("creation")]
    [SerializeField] Color baseColor1, baseColor2, borderColor, shineColor, enemyColor, allyColor;

    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private GameObject popcornSprite;
    [SerializeField] private GameObject acidSprite;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject onHover;
    [SerializeField] private new BoxCollider2D collider;

    [Header("InGame")]
    public new string name;
    public string effects;

    public string type;
    public bool spawnBlocks;

    public int positionX, positionY;
    private bool clickable = false;
    public bool isInRange = false;
    public bool isWalkable;
    public bool isAcided;
    public bool addsWater;
    public bool dealsDamage;
    public bool buffsEntity;
    public bool healsEntity;
    public bool isPopCorned;
    public int initTurnPop;
    public int initTurnAcid;
    [SerializeField] private int WATER = 0;
    [SerializeField] private int DAMAGE = 0;
    [SerializeField] private int BUFF = 0;
    [SerializeField] private int HEAL = 0;
    public GameObject entity;
    public int weight;

    [SerializeField] private GameObject[] seeds;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        popcornSprite = transform.GetChild(1).gameObject;
        if(tag == "NotWalkable") { return; }
        acidSprite = transform.GetChild(2).gameObject;
    }

    public void GetWater()
    {
        if (positionX + 1 < GridCreator.instance.x && GridCreator.instance.GetTile(positionX + 1, positionY).GetComponent<Tile>().type == "water")
        {
            addsWater = true;
        }
        if (positionX - 1 >= 0 && GridCreator.instance.GetTile(positionX - 1, positionY).GetComponent<Tile>().type == "water")
        {
            addsWater = true;
        }
        if (positionY + 1 < GridCreator.instance.y && GridCreator.instance.GetTile(positionX, positionY + 1).GetComponent<Tile>().type == "water")
        {
            addsWater = true;
        }
        if (positionY - 1 >= 0 && GridCreator.instance.GetTile(positionX, positionY - 1).GetComponent<Tile>().type == "water")
        {
            addsWater = true;
        }
    }
    public void Init(int color, int x, int y)
    {
        //renderer.color = (color == 0) ? baseColor1 : baseColor2;
        //renderer.sprite = tileSprites[0];
        positionX = x;
        positionY = y;
    }

    private void Update()
    {
        CheckTurnPop();
        CheckTurnAcid();
        StopShine();
        SetClickable(false);
        SelectShine(GameController.instance.SelectedPlayer()); 
        if(entity == null && tag != "NotWalkable")
        {
            isWalkable = true;
        }
        else
        {
            isWalkable = false;
        }
    }
    private void CheckTurnPop()
    {
        if (!isPopCorned)
        {
            popcornSprite.SetActive(false);
            initTurnPop = -1;
            return;
        }
        if(initTurnPop != -1)
        {
            if (GameController.instance.turn - initTurnPop > GameController.instance.CharacterCount())
            {

                isPopCorned = false;
            }

        }
        else
        {
            popcornSprite.SetActive(true);
            initTurnPop = GameController.instance.turn;
        }
    }
    private void CheckTurnAcid()
    {
        if(acidSprite == null)
        {
            return;
        }
        if (!isAcided)
        {
            acidSprite.SetActive(false);
            initTurnAcid = -1;
            return;
        }
        if (initTurnAcid != -1)
        {
            if (GameController.instance.turn - initTurnAcid > GameController.instance.CharacterCount())
            {

                isAcided = false;
            }

        }
        else
        {
            acidSprite.SetActive(true);
            initTurnAcid = GameController.instance.turn;
        }
    }
    public void GenerateSeed()
    {
        if(seeds.Length == 0)
        {
            return;
        }
        if(Random.Range(0f, 100f) < 5f)
        {
            GridCreator.instance.seeds.Add(Instantiate(seeds[Random.Range(0, seeds.Length)], transform, false));
           // Instantiate(seeds[Random.Range(0, seeds.Length)], transform, false);
        }
    }
    public void SelectShine(GameObject player)
    {
        if (player.tag == "Enemy")
        {
            return;
        }
        if (entity != null && player.GetComponent<Entity>().actualState == Entity.EntityState.READYTOATTACK && player.tag == "Player" && Mathf.Abs(positionX - player.GetComponent<Entity>().gridX) + Mathf.Abs(positionY - player.GetComponent<Entity>().gridY) <= player.GetComponent<Entity>().mainAttack.range)
        {
            ShineEntity();
            SetClickable(true);
        }
        if (player.GetComponent<Entity>().actualState == Entity.EntityState.USINGSKILL)
        {
            if (entity == null && player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].selectsStightTile && Mathf.Abs(positionX - player.GetComponent<Entity>().gridX) + Mathf.Abs(positionY - player.GetComponent<Entity>().gridY) <= player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].range)
            {
                if (player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].destinationTile == null)
                {
                    ShineStraightLine();
                }
            }
            else if (entity == null && player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].selectsTile && Mathf.Abs(positionX - player.GetComponent<Entity>().gridX) + Mathf.Abs(positionY - player.GetComponent<Entity>().gridY) <= player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].range)
            {
                if (player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].destinationTile == null)
                {
                    ShineTile();
                    SetClickable(true);
                }
            }
            else if (entity != null && !player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].isOnDestination && Mathf.Abs(positionX - player.GetComponent<Entity>().gridX) + Mathf.Abs(positionY - player.GetComponent<Entity>().gridY) <= player.GetComponent<Entity>().skills[player.GetComponent<Entity>().skillSelected].range)
            {
                ShineEntity();
            }
        }
        if (isInRange && isWalkable && player.tag == "Player" && player.GetComponent<Entity>().actualState == Entity.EntityState.READYTOMOVE)
        {
            ShineTile();
            SetClickable(true);
            isInRange = false;
        }
        if (isInRange && isWalkable && player.tag == "Carriage" && GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.READYTOMOVE && tag == "PathTile")
        {
            ShineTile();
            SetClickable(true);
            isInRange = false;
        }
    }
    public void ShineTile()
    {
        
        renderer.color = shineColor;
                
    }
    public void ShineEntity()
    {
        if(GameController.instance.SelectedPlayer().tag != "Player")
        {
            return;
        }
        switch (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState)
        {
            case Entity.EntityState.READYTOATTACK:
                if (GameController.instance.SelectedPlayer().GetComponent<Plants>().attacked)
                {
                    return;
                }
                if (entity.tag == "Enemy" && !GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.directedToAlly)
                {
                    renderer.color = enemyColor;
                }
                else if (entity.tag == "Player" && GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.directedToAlly)
                {
                    renderer.color = allyColor;
                }
                break;
            case Entity.EntityState.USINGSKILL:
                if (GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].currentCoolDown > 0)
                {

                }
                if (entity.tag == "Enemy" && GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isAttacking)
                {
                    renderer.color = enemyColor;
                }
                if (entity.tag == "Player" && GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing)
                {
                    renderer.color = allyColor;
                }
                break;
        }
    }
    private void ShineStraightLine()
    {
        if(positionX == GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX || positionY == GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY)
        {
            SetClickable(true);
            ShineTile();
        }
    }
    public void SetClickable(bool isCliclabke)
    {
        clickable = isCliclabke;
    }
    public void StopShine()
    {
        renderer.color = baseColor1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "NotWalkable")
        {
            return;
        }
        if (entity != null)
        {
            return;
        }
        if (collision.tag == "Feet")
        {
            entity = collision.gameObject.transform.parent.gameObject;

            entity.GetComponent<Entity>().SetTile(gameObject);
            //isAcided = false;
            if (dealsDamage)
            {
                collision.GetComponentInParent<Entity>().Damage(DAMAGE);
            }
            if (buffsEntity)
            {
                collision.GetComponentInParent<Entity>().AttackBust(BUFF);
            }
            if (healsEntity)
            {
                collision.GetComponentInParent<Entity>().Heal(HEAL);
            }
            if (isPopCorned)
            {
                collision.GetComponentInParent<Entity>().AttackBust(10);
                collision.GetComponentInParent<Entity>().DefenseBust(10);
            }
            if(collision.GetComponentInParent<Entity>().actualState == Entity.EntityState.USINGSKILL && collision.GetComponentInParent<Entity>().skills[collision.GetComponentInParent<Entity>().skillSelected].name == "Acid")
            {
                SetAcid();
                weight = 4;
                Debug.Log("ACID :  " + gameObject.name);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(tag == "NotWalkable")
        {
            return;
        }
        if(collision.tag == "Feet")
        {
            if (collision.gameObject.transform.parent.gameObject != entity)
            {
                return;
            }
            
            entity = null;
            if(collision.GetComponentInParent<Entity>().actualState == Entity.EntityState.MOVEING || collision.GetComponentInParent<Entity>().actualState == Entity.EntityState.ATTACKING || collision.GetComponentInParent<Entity>().actualState == Entity.EntityState.USINGSKILL || collision.GetComponentInParent<Entity>().actualState == Entity.EntityState.FINISHED)
            {
                isWalkable = true;
            }
        }
    }
    private void OnMouseEnter()
    {
        ShowTile();
    }
    private void OnMouseExit()
    {
        HideTile();
    }
    public void ShowTile()
    {
        TilePanel.instance.Hide();
        TilePanel.instance.Change(gameObject);
        onHover.SetActive(true);
    }
    public void HideTile()
    {
        TilePanel.instance.Hide();
        onHover.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (UIHoverListener.instance.isUIOverride)
        {
            return;
        }
        if (entity != null)
        {
            return;
        }
        if (!clickable)
        {
            return;
        }
        if(GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.READYTOMOVE)
        {
            GameController.instance.SelectedPlayer().GetComponent<Entity>().SetDestination(gameObject);
            return;
        }
        if(entity != null)
        {
            if (GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.READYTOATTACK &&  (GameController.instance.SelectedPlayer().GetComponent<Entity>().mainAttack.directedToAlly) ? ((entity.tag == "Player") ? true: false) : ((entity.tag == "Enemy") ? true : false))
            {
                GameController.instance.SelectedPlayer().GetComponent<Entity>().mainObjective = entity;
                GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState = Entity.EntityState.ATTACKING;
                return;
            }
        }
        if(GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.USINGSKILL)
            if (GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[GameController.instance.SelectedPlayer().GetComponent<Entity>().skillSelected].selectsTile || GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[GameController.instance.SelectedPlayer().GetComponent<Entity>().skillSelected].selectsStightTile)
            {
                GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[GameController.instance.SelectedPlayer().GetComponent<Entity>().skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
                GameController.instance.SelectedPlayer().GetComponent<Entity>().mainObjective = gameObject;
                return;
            }
    }

    private void SetAcid()
    {
        initTurnAcid = -1;
        isAcided = true;
    }
    public int GetX()
    {
        return positionX;
    }
    public int GetY()
    {
        return positionY;
    }
}
