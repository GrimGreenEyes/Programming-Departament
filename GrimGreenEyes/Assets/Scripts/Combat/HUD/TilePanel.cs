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
    [SerializeField] private GameObject entityPoison;
    [SerializeField] private GameObject entityBleed;
    [SerializeField] private GameObject entityStun;
    [SerializeField] private TMP_Text attText;
    [SerializeField] private TMP_Text defText;
    [SerializeField] private TMP_Text hResText;
    [SerializeField] private TMP_Text cResText;
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
            entityStun.SetActive((newTile.GetComponent<Tile>().entity.GetComponent<Entity>().stuned) ? true : false);
            entityBleed.SetActive((newTile.GetComponent<Tile>().entity.GetComponent<Entity>().bleeding) ? true : false);
            entityPoison.SetActive((newTile.GetComponent<Tile>().entity.GetComponent<Entity>().poisoned) ? true : false);
            attText.SetText(newTile.GetComponent<Tile>().entity.GetComponent<Entity>().attack.ToString());
            defText.SetText(newTile.GetComponent<Tile>().entity.GetComponent<Entity>().defense.ToString());
            hResText.SetText(newTile.GetComponent<Tile>().entity.GetComponent<Entity>().heatResistance.ToString());
            cResText.SetText(newTile.GetComponent<Tile>().entity.GetComponent<Entity>().freezeResistance.ToString());
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
