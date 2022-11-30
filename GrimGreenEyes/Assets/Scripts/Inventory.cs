using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryObject, debugPanel, inventoryAndBlenderObject; //Objeto padre del panel del inventario
    [SerializeField] private List<InventorySlot> slotsList = new List<InventorySlot>(); //Almacena los SLOTS (objetos con botones) del inventario
    public Dictionary<Item, int> inventoryItems = new Dictionary<Item, int>(); //Lista de ITEMS que posee el jugador
    [SerializeField] private Blender blender;
    [SerializeField] private PlantsManager plantsManager;

    [SerializeField] private OptionsPanel optionsPanel;
    [SerializeField] private UIManager uiManager;


    void Start() //Indexa todos los slots en una lista
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        foreach (Transform child in inventoryObject.transform)
        {
            slotsList.Add(child.GetComponent<InventorySlot>());
        }
        UpdateInventory();

        CloseDebugPanel(); //DEBUGGING
    }
    
    public void UpdateInventory() //Aplica los ITEMS en el diccionario al GRID
    {
        foreach (InventorySlot slot in slotsList)
        {
            slot.ResetSlot();
        }

        int i = 0;
        foreach(KeyValuePair<Item, int> entry in inventoryItems)
        {
            slotsList[i].SetItemAndAmount(entry.Key, entry.Value);
            i++;
        }
    }


    public void AddItem(Item item) //Añade un item al diccionario (inventario). Si ese item ya existía, aumenta su cantidad en 1 unidad.
    {
        if (inventoryItems.ContainsKey(item))
        {
            inventoryItems[item] = inventoryItems[item] + 1;
        }
        else
        {
            inventoryItems.Add(item, 1);
        }
        UpdateInventory();
    }

    public void RemoveItem(Item item) //Reduce en 1 unidad la cantidad del item indicado. Si la cantidad llega a 0, el item es borrado de la lista.
    {
        if (!inventoryItems.ContainsKey(item))
        {
            Debug.Log("THE ITEM " + item.name + " DOES NOT EXIST IN THE INVENTORY");
            return;
        }

        inventoryItems[item] = inventoryItems[item] - 1;
        if(inventoryItems[item] <= 0)
        {
            inventoryItems.Remove(item);
        }
        UpdateInventory();
    }

    public void OpenInventory()
    {
        inventoryAndBlenderObject.SetActive(true);
        GameObject.Find("UIManager").GetComponent<UIManager>().HideExitButton();
    }

    public void CloseInventory()
    {
        optionsPanel.HideOptionsPanel();
        blender.RemoveItemInSlot(0);
        blender.RemoveItemInSlot(1);
        inventoryAndBlenderObject.SetActive(false);
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowExitButton();
    }

    public void TestAddItem(Item item)
    {
        AddItem(item);
    }

    public void CloseDebugPanel()
    {
        debugPanel.SetActive(false);
    }

    public void OpenDebugPanel()
    {
        debugPanel.SetActive(true);
    }

    public void OpenOptionsPanel(Vector3 position, Item item)
    {
        optionsPanel.ShowOptionsPanel(position, item);
    }

    public bool AddToBlender(Item item)
    {
        if (blender.isBlenderFull())
        {
            return false;
        }
        else
        {
            RemoveItem(item);
            blender.AddItem(item);
            return true;
        }
    }

    public void UnselectAllSlots()
    {
        foreach(InventorySlot slot in slotsList)
        {
            slot.DefaultSlot();
        }
    }

    public void PlantSeed(Item item)
    {
        if (!plantsManager.AnyFreePot())
        {
            uiManager.ShowWarning("No hay ningún macetero libre", 1.5f);
        }
        else
        {
            CloseInventory();
            plantsManager.PlantSeed(item);
        }
    }

}
