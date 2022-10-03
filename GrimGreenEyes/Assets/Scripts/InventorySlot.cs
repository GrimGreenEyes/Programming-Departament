using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image itemImage;
    private int itemAmount = 1;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    [SerializeField] private Item voidItem;
    private OptionsPanel optionsPanel;
    private Inventory inventoryManager;

    private void Start()
    {
        optionsPanel = GameObject.Find("OptionsPanel").GetComponent<OptionsPanel>();
        inventoryManager = GameObject.Find("InventoryPanel").GetComponent<Inventory>();
    }

    public void SetItem(Item newItem) //Pone un item en el slot de inventario. Además, actualiza la imagen.
    {
        item = newItem;
        itemImage.sprite = item.sprite;
    }

    public void SetAmount(int amount) //Pone una cantidad de items en el slot. Si esa cantidad es 1 o menos, no muestra nada.
    {
        itemAmount = amount;
        if(itemAmount <= 1)
        {
            itemAmountText.text = "";
        }
        else
        {
            itemAmountText.text = itemAmount.ToString();
        }
    }

    public void SetItemAndAmount(Item newItem, int amount) //Pone ambos tipo de item y cantidad.
    {
        SetItem(newItem);
        SetAmount(amount);
    }

    public void ResetSlot()
    {
        SetItemAndAmount(voidItem, 0);
    }

    public Item GetItem() //Devuelve el item en ese slot.
    {
        return item;
    }

    public void ClickSlot() 
    {
        if (item.name == voidItem.name || item == null)
        {
            optionsPanel.HideOptionsPanel();
            return;
        }
            

        inventoryManager.OpenOptionsPanel(this.transform.position, item);
    }


}
