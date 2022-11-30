using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private GameObject parentPanel, optionButtonPrefab;
    private List<GameObject> OptionButtons = new List<GameObject>();
    public Inventory inventoryManager;
    [SerializeField] private TextMeshProUGUI nameLabel;
    private Item currentItem;
    private RectTransform thisTransform, canvasTransform;
    private Canvas myCanvas;


    private void Start()
    {
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        thisTransform = GetComponent<RectTransform>();
        canvasTransform = myCanvas.GetComponent<RectTransform>();
        //HideOptionsPanel();
    }

    private void Update()
    {
        while (thisTransform.anchoredPosition.x + thisTransform.sizeDelta.x >= canvasTransform.sizeDelta.x / 2)
        {
            transform.position = transform.position - new Vector3(0.1f, 0, 0);
        }
    }

    public void ShowOptionsPanel(Vector3 pos, Item item) //All the options displayed when clicking the item
    {
        currentItem = item;
        gameObject.SetActive(true);
        transform.position = pos;

        


        foreach (GameObject button in OptionButtons)
        {
          Destroy(button);
        }
        OptionButtons.Clear();

        SetLabel(item.name);
        AddOption(Strings.ADD_TO_BLENDER);
        if (item.type == Items.SEED)
        {
            AddOption(Strings.PLANT);
        }
    }

    public void HideOptionsPanel()
    {
        gameObject.SetActive(false);
        inventoryManager.UnselectAllSlots();
    }

    private void SetLabel(string name)
    {
        nameLabel.text = currentItem.name;
    }

    private void AddOption(string option)
    {
            GameObject newButton = Instantiate(optionButtonPrefab);
            OptionButtons.Add(newButton);
            newButton.transform.SetParent(this.transform);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.GetComponent<OptionButton>().SetName(option);
            switch (option)
            {
                case Strings.ADD_TO_BLENDER:
                    newButton.GetComponent<Button>().onClick.AddListener(ClickAddToBlender);
                    break;
                case Strings.PLANT:
                    newButton.GetComponent<Button>().onClick.AddListener(ClickPlant);
                    break;
            }
    }

    public void ClickAddToBlender()
    {
        if (inventoryManager.AddToBlender(currentItem))
        {
            HideOptionsPanel();
        }
    }

    public void ClickPlant()
    {
        inventoryManager.PlantSeed(currentItem);
        HideOptionsPanel();
    }



}
