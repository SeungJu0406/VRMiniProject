using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


[System.Serializable]
public struct IngredientInfo
{
    public IngredientData data;
    public int count;
}

public class Plate : MonoBehaviour
{
    [SerializeField] List<IngredientInfo> stackList = new List<IngredientInfo>(10);

    [SerializeField] Ingredient topIngredient;
    [SerializeField] Ingredient bottomIngredient;

    public void AddStack(Ingredient ingredient)
    {
        ProcessAddStack(ingredient);

        int index = stackList.FindIndex(info => info.data.Equals(ingredient.data));
        if (index != -1)
        {          
            IngredientInfo temp = stackList[index];
            temp.count++;
            stackList[index] = temp;
        }
        else
        {
            IngredientInfo ingredientInfo = new IngredientInfo();
            ingredientInfo.data = ingredient.data;
            ingredientInfo.count = 1;
            stackList.Add(ingredientInfo);
        }
    }

    public void RemoveStack(Ingredient ingredient)
    {
        int index = stackList.FindIndex(info => info.data.Equals(ingredient.data));
        
        if (stackList[index].count > 1)
        {
            IngredientInfo temp = stackList[index];
            temp.count--;
            stackList[index] = temp;
        }
        else if (stackList[index].count == 1)
        {
            stackList.RemoveAt(index);
        }

        ProcessToRemoveStack(ingredient);
    }
    void ProcessAddStack(Ingredient ingredient)
    {
        ingredient.parent = topIngredient;
        if (ingredient.parent != null)
        {        
            ingredient.parent.child = ingredient;
        }

        if (stackList.Count == 0)
            bottomIngredient = ingredient;
        topIngredient = ingredient;


    }
    void ProcessToRemoveStack(Ingredient ingredient)
    {
        if(stackList.Count == 0)
            bottomIngredient = null;
        topIngredient = ingredient.parent;
        if(ingredient.parent != null)
        {
            ingredient.parent.child = null;
            ingredient.parent = null;
        }           
    }
}
