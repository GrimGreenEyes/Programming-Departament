using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    [SerializeField] public List<BookPage> pageList = new List<BookPage>();
    public int position = 0;
    [SerializeField] private GameObject nextButton, prevButton, closeButton;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText;
    [SerializeField] private Image pageImage;
    private GlobalVar globalVar;
    private UIManager uiManager;

    public void OpenBook()
    {
        if (uiManager == null) uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.PlaySoundBook();
        gameObject.SetActive(true);
        position = 0;
        UpdateVisuals();
        GameObject.Find("UIManager").GetComponent<UIManager>().HideExitButton();
    }

    public void CloseBook()
    {
        uiManager.PlaySoundBook();
        gameObject.SetActive(false);
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowExitButton();
    }

    public void NextPage()
    {
        uiManager.PlaySoundBook();
        position++;
        UpdateVisuals();
    }

    public void PrevPage()
    {
        uiManager.PlaySoundBook();
        position--;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        nextButton.SetActive(position < pageList.Count-1);
        prevButton.SetActive(position > 0);
        closeButton.SetActive(!(position < pageList.Count - 1));
        titleText.text = pageList[position].title;
        descriptionText.text = pageList[position].description;
        pageImage.sprite = pageList[position].image;
    }
}


[System.Serializable]
public class BookPage
{
    public string title;
    [TextArea(3, 6)]
    public string description;
    public Sprite image;
}
