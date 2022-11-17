using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesList : MonoBehaviour
{
    public GameObject recipeListPrefab;
    public Blender blender;
    public GameObject skillsFertList, statsFertList;
    private bool first = true;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void OpenRecipesList()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        gameObject.SetActive(true);
        if (first)
        {
            UpdateVisuals();
            first = false;
        }
        
    }

    public void CloseRecipesList()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisuals()
    {
        foreach(Recipe recipe in blender.recipeList)
        {
            GameObject newItem = Instantiate(recipeListPrefab);
            newItem.GetComponent<RecipeItem>().Initialize(recipe.input0, recipe.input1, recipe.output);

            switch (recipe.output.type)
            {
                case 0:
                    newItem.transform.SetParent(statsFertList.transform);
                    break;
                case 1:
                    newItem.transform.SetParent(skillsFertList.transform);
                    break;
            }

            newItem.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
