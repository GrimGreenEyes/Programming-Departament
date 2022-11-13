using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeItem : MonoBehaviour
{
    public Image input1, input2, output;

    public void Initialize(Item item1, Item item2, Fertilizer fertilizer)
    {
        input1.sprite = item1.sprite;
        input1.gameObject.GetComponent<HoverElement>().displayText = item1.name;

        input2.sprite = item2.sprite;
        input2.gameObject.GetComponent<HoverElement>().displayText = item2.name;

        output.sprite = fertilizer.sprite;
        output.gameObject.GetComponent<HoverElement>().displayText = fertilizer.name;
    }
}
