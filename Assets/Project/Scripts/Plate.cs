using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;


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
    public Ingredient TopIngredient { get { return topIngredient; } set { topIngredient = value; OnChangeTop?.Invoke(this); } }
    public event UnityAction<Plate> OnChangeTop;
    [SerializeField] Ingredient bottomIngredient;
    public Ingredient BottomIngredient { get {  return bottomIngredient; } set { bottomIngredient = value; OnChangeBottom?.Invoke(this); } } 
    public event UnityAction<Plate> OnChangeBottom;
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
        ingredient.SubscribePlateEvent(this);

        ingredient.parent = TopIngredient;
        if (ingredient.parent != null)
        {        
            ingredient.parent.child = ingredient;
        }

        if (stackList.Count == 0)
            BottomIngredient = ingredient;
        TopIngredient = ingredient;

        
    }
    void ProcessToRemoveStack(Ingredient ingredient)
    {
        ingredient.UnSubscribePlateEvent(this);

        if (stackList.Count == 0)
            BottomIngredient = null;
        TopIngredient = ingredient.parent;
        if(ingredient.parent != null)
        {
            ingredient.parent.child = null;
            ingredient.parent = null;
        }      
    }
}
