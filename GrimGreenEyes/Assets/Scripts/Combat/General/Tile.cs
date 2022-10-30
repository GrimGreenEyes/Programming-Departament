using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("creation")]
    [SerializeField] Color baseColor1, baseColor2, borderColor, shineColor, enemyColor, allyColor;

    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject onHover;
    [SerializeField] private new BoxCollider2D collider;

    [Header("InGame")]
    public string description;

    private int positionX, positionY;
    private bool clickable = false;
    public bool isInRange = false;
    public bool isWalkable;
    public GameObject entity;
    public int weight;

    [SerializeField] private GameObject[] seeds;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
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
        StopShine();
        SetClickable(false);
        if(GameController.instance.SelectedPlayer().tag != "Player")
        {
            return;
        }
        if(entity != null && GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.IDLE && Mathf.Abs(positionX  - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX) + Mathf.Abs(positionY - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY) <= GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.range)
        {
            ShineEntity();
        }
        if(entity != null && GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.USINGSKILL && Mathf.Abs(positionX - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX) + Mathf.Abs(positionY - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY) <= GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].radious)
        {
            ShineEntity();
        }
        if (isInRange && isWalkable && GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.IDLE)
        {
            ShineTile();
            SetClickable(true);
            isInRange = false;
        }
    }

    public void GenerateSeed()
    {
        if(seeds.Length == 0)
        {
            return;
        }
        if(entity != null)
        {
            return;
        }
        Instantiate(seeds[Random.Range(0, seeds.Length)], transform, false);
    }
    public void ShineTile()
    {
        //if (isInRange && isWalkable)
        //{
        //    return;
        //}
        
        renderer.color = shineColor;
                
    }
    public void ShineEntity()
    {
        switch (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState)
        {
            case Entity.EntityState.IDLE:
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
                if (entity.tag == "Enemy" && !GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing)
                {
                    renderer.color = enemyColor;
                }
                else if (entity.tag == "Player" && GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing)
                {
                    renderer.color = allyColor;
                }
                break;
        }
    }
    public void SetClickable(bool isCliclabke)
    {
        clickable = isCliclabke;
    }
    public void StopShine()
    {
        renderer.color = (((positionX + positionY) % 2) == 0)? baseColor1 : baseColor2;
        clickable = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            entity = collision.gameObject.transform.parent.gameObject;
            isWalkable = false;
            entity.GetComponent<Entity>().SetTile(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            entity = null;
            isWalkable = true;
        }
    }
    private void OnMouseEnter()
    {
        if(entity != null)
        {
            return;
        }
        if(!isWalkable)
        {
            return;
        }
        TilePanel.instance.Hide();
        TilePanel.instance.Change(gameObject);
        onHover.SetActive(true);
    }
    private void OnMouseExit()
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
        if(this.transform.childCount != 1)
        {
            return;
        }
        /*if (!GameController.instance.IsPointerOverUIObject(Input.GetTouch(0)))
        {
        }*/
        GameController.instance.SelectedPlayer().GetComponent<Plants>().SetDestination(gameObject);
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
