using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("creation")]
    [SerializeField] Color baseColor1, baseColor2, borderColor, shineColor, enemyColor;

    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject onHover;
    [SerializeField] private new BoxCollider2D collider;

    [Header("InGame")]
    public string description;

    private int positionX, positionY;
    private bool clickable = false;
    private GameObject entity;

    public void Init(int color, int x, int y)
    {
        renderer.color = (color == 0) ? baseColor1 : baseColor2;
        renderer.sprite = tileSprites[0];
        positionX = x;
        positionY = y;
    }
    public void SetBorder(int border)
    {
        renderer.color = borderColor;
        renderer.sprite = tileSprites[border];
        collider.enabled = false;
    }
    public void ShineTile()
    {
        if (this.transform.childCount != 2)
        {
            return;
        }
        renderer.color = shineColor;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            entity = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            entity = null;
        }
    }
    private void OnMouseEnter()
    {
        if(entity != null)
        {
            return;
        }
        if (this.transform.childCount != 3)
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
        if (entity != null)
        {
            return;
        }
        if (!clickable)
        {
            return;
        }
        if(this.transform.childCount != 2)
        {
            return;
        }
        /*if (!GameController.instance.IsPointerOverUIObject(Input.GetTouch(0)))
        {
        }*/
        GameController.instance.SelectedPlayer().GetComponent<Plants>().SetDestination(this);
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
