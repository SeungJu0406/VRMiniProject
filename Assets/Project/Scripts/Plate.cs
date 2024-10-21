using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;
using System.Text;
using UnityEngine.XR.Interaction.Toolkit;


[System.Serializable]
public struct IngredientInfo
{
    public IngredientData Data;
    public int Count;
}

public class Plate : MonoBehaviour
{
    [SerializeField] List<IngredientInfo> _stackList = new List<IngredientInfo>(10);

    [SerializeField] Ingredient _topIngredient;
    public Ingredient TopIngredient { get { return _topIngredient; } set { _topIngredient = value; OnChangeTop?.Invoke(this); } }
    public event UnityAction<Plate> OnChangeTop;
    [SerializeField] Ingredient _bottomIngredient;
    public Ingredient BottomIngredient { get {  return _bottomIngredient; } set { _bottomIngredient = value; OnChangeBottom?.Invoke(this); } } 
    public event UnityAction<Plate> OnChangeBottom;

    int _plateLayer;
    int _ignoreLayer;
    int _socketLayer;

    StringBuilder _sb = new StringBuilder();

    private void Awake()
    {
        _plateLayer = LayerMask.NameToLayer("Plate");
        _ignoreLayer = LayerMask.NameToLayer("Ignore Collision");
        _socketLayer = LayerMask.NameToLayer("Socket");
    }

    public void OnSelectedEnter(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.layer != _socketLayer)
        {
            gameObject.layer = _ignoreLayer;
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.layer != _socketLayer)
        {
            gameObject.layer = _plateLayer;
        }
    }

        public void AddStack(Ingredient ingredient)
    {
        ProcessAddStack(ingredient);

        int index = _stackList.FindIndex(info => info.Data.Equals(ingredient.Data));
        if (index >= _stackList.Count) return;

        if (index != -1)
        {          
            IngredientInfo temp = _stackList[index];
            temp.Count++;
            _stackList[index] = temp;
        }
        else
        {
            IngredientInfo ingredientInfo = new IngredientInfo();
            ingredientInfo.Data = ingredient.Data;
            ingredientInfo.Count = 1;
            _stackList.Add(ingredientInfo);
        }
    }

    public void RemoveStack(Ingredient ingredient)
    {
        int index = _stackList.FindIndex(info => info.Data.Equals(ingredient.Data));
        if (index != -1)
        {
            if (_stackList[index].Count > 1)
            {
                IngredientInfo temp = _stackList[index];
                temp.Count--;
                _stackList[index] = temp;
            }
            else if (_stackList[index].Count == 1)
            {
                _stackList.RemoveAt(index);
            }
        }
        ProcessToRemoveStack(ingredient);
    }
    public string GetValueToString()
    { 
        _stackList.Sort((s1,s2) => s1.Data.ID.CompareTo(s2.Data.ID));
        _sb.Clear();
        foreach (IngredientInfo ingredient in _stackList) 
        {
            _sb.Append($"{ingredient.Data.Name},{ingredient.Count}");
        }
        return _sb.ToString();
    }

    void ProcessAddStack(Ingredient ingredient)
    {
        if (ingredient == null) return;

        ingredient.SubscribePlateEvent(this);

        ingredient.Parent = TopIngredient;
        if (ingredient.Parent != null)
        {        
            ingredient.Parent.Child = ingredient;
        }

        if (_stackList.Count == 0)
            BottomIngredient = ingredient;
        TopIngredient = ingredient;

        
    }
    void ProcessToRemoveStack(Ingredient ingredient)
    {
        if(ingredient == null) return;

        ingredient.UnSubscribePlateEvent(this);

        if (_stackList.Count == 0)
            BottomIngredient = null;
        TopIngredient = ingredient.Parent;
        if(ingredient.Parent != null)
        {
            ingredient.Parent.Child = null;
            ingredient.Parent = null;
        }      
    }
}
