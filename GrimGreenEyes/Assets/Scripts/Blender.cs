using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipeList = new List<Recipe>();
    [SerializeField] private GameObject[] inputSlots = new GameObject[2];
    [SerializeField] private GameObject outputSlot;
    private Item[] blenderStorage = new Item[2];
    [SerializeField] private Item voidItem;
    [SerializeField] private Inventory inventoryManager;
    [SerializeField] private GameObject blendButton;
    [SerializeField] private Sprite nullRecipeSprite;

    private void Start()
    {
        blenderStorage[0] = voidItem;
        blenderStorage[1] = voidItem;
        UpdateBlender();    
    }

    public void UpdateBlender()
    {
        inputSlots[0].transform.GetChild(0).GetComponent<Image>().sprite = blenderStorage[0].sprite;
        inputSlots[1].transform.GetChild(0).GetComponent<Image>().sprite = blenderStorage[1].sprite;
        outputSlot.transform.GetChild(0).GetComponent<Image>().sprite = voidItem.sprite;
        blendButton.SetActive(false);
        CheckRecipe();
    }

    public bool isBlenderFull()
    {
        return (blenderStorage[0] != voidItem && blenderStorage[1] != voidItem);
    }

    public void AddItem(Item item)
    {
        if (isBlenderFull()) return; //Nunca debería llamarse a este método sin comprobar si está lleno, pero lo vuelvo a comprobar por si acaso.
        if (blenderStorage[0] == voidItem)
        {
            blenderStorage[0] = item;
        }
        else
        {
            blenderStorage[1] = item;
        }
        UpdateBlender();
    }

    public void RemoveItemInSlot(int slot)
    {
        if (blenderStorage[slot] == voidItem) return;
        inventoryManager.AddItem(blenderStorage[slot]);
        blenderStorage[slot] = voidItem;
        UpdateBlender();
    }

    public void UseItemInSlot(int slot)
    {
        blenderStorage[slot] = voidItem;
        UpdateBlender();
    }

    private void CheckRecipe()
    {
        bool recipeFound = false;
        if (blenderStorage[0] == voidItem || blenderStorage[1] == voidItem) return; //Solo comprueba crafteos si hay 2 items en el input
        foreach(Recipe recipe in recipeList)
        {
            if((blenderStorage[0] == recipe.input0 && blenderStorage[1] == recipe.input1) || (blenderStorage[1] == recipe.input0 && blenderStorage[0] == recipe.input1))
            {
                outputSlot.transform.GetChild(0).GetComponent<Image>().sprite = recipe.output.sprite;
                blendButton.SetActive(true);
                recipeFound = true;
                break;
            }
        }
        if (!recipeFound)
        {
            outputSlot.transform.GetChild(0).GetComponent<Image>().sprite = nullRecipeSprite;
        }

    }

    public void ClickBlend()
    {
        UseItemInSlot(0);
        UseItemInSlot(1);
    }
}

[System.Serializable]
public class Recipe
{
    public Item input0, input1, output;
}
