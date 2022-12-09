using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TilePanel : MonoBehaviour
{
    public static TilePanel instance;

    [SerializeField] private GameObject entityPanel;
    [SerializeField] private Image entityImage;
    [SerializeField] private TMP_Text entityText;
    [SerializeField] private Image tileImage;
    [SerializeField] private new TMP_Text name;
    [SerializeField] private TMP_Text description;

    private void Awake()
    {
        if( instance == null)
        {
            instance = this;
        }
        Hide();
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    public void Change(GameObject newTile)
    {
        entityPanel.SetActive(false);
        if(newTile.GetComponent<Tile>().entity != null)
        {
            entityPanel.SetActive(true);
            entityImage.sprite = newTile.GetComponent<Tile>().entity.GetComponent<Entity>().defaultSprite;
            entityText.SetText(newTile.GetComponent<Tile>().entity.GetComponent<Entity>().name);
        }
        tileImage.sprite = newTile.GetComponent<SpriteRenderer>().sprite;
        name.SetText(newTile.GetComponent<Tile>().name);
        description.SetText(newTile.GetComponent<Tile>().effects);
    }
    public void Hide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
