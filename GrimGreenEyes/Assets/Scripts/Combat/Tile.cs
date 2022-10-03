using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Color baseColor1, baseColor2, borderColor;

    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject onHover;

    public void Init(int color)
    {
        renderer.color = (color == 0) ? baseColor1 : baseColor2;
        renderer.sprite = tileSprites[0];
    }
    public void SetBorder(int border)
    {
        renderer.color = borderColor;
        renderer.sprite = tileSprites[border];
    }
    private void OnMouseEnter()
    {
        onHover.SetActive(true);
        
    }
    private void OnMouseExit()
    {
        onHover.SetActive(false);
    }
}
