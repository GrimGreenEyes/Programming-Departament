using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("creation")]
    [SerializeField] Color baseColor1, baseColor2, borderColor;

    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject onHover;
    [SerializeField] private GameObject shineLayer;
    [SerializeField] private BoxCollider2D collider;

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
    private void Update()
    {
        if(entity == GameController.instance.SelectedPlayer())
        {
            GridCreator.instance.ShineTiles(positionX, positionY, 5);
        }
    }

    public void ShineTile()
    {
        if (this.transform.childCount != 2)
        {
            return;
        }
        shineLayer.SetActive(true);
        clickable = true;
    }
    public void StopShine()
    {
        shineLayer.SetActive(false);
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
        if (!clickable)
        {
            return;
        }
        if (this.transform.childCount != 2)
        {
            return;
        }
        onHover.SetActive(true);
    }
    private void OnMouseExit()
    {
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
        GameController.instance.SelectedPlayer().transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
    }

}
