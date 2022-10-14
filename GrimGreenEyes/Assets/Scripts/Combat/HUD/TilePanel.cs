using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TilePanel : MonoBehaviour
{
    public static TilePanel instance;

    [SerializeField] private Image image;
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
        image.sprite = newTile.GetComponent<Sprite>();
        description.SetText(newTile.GetComponent<Tile>().description);
    }
    public void Hide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}