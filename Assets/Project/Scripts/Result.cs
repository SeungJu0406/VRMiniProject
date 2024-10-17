using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Result : MonoBehaviour
{
    [Header("기본 레시피")]
    [SerializeField] RecipeData _recipeData;
    [Header("햄버거 빵 체크")]
    [SerializeField] IngredientData _bottomData;
    [SerializeField] IngredientData _topData;


    [SerializeField] List<IngredientInfo> _resultList = new List<IngredientInfo>(10);
    Plate _resultPlate;

    StringBuilder _sb = new StringBuilder();

    private void Start()
    {
        InitRecipe();
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        _resultPlate = args.interactableObject.transform.GetComponent<Plate>();
        ProcessResult();
    }

    void ProcessResult()
    {
        if (_resultPlate.BottomIngredient?.Data == _bottomData && _resultPlate.TopIngredient?.Data == _topData )
        {
            string plate = _resultPlate.GetValueToString();
            string result = GetValueToString();
            if (plate.Equals(result))
            {
                Debug.Log("성공!");
                return;
            }
        }
        Debug.Log("실패");
    }

    string GetValueToString()
    {
        _resultList.Sort((s1, s2) => s1.Data.ID.CompareTo(s2.Data.ID));
        _sb.Clear();
        foreach (IngredientInfo ingredient in _resultList)
        {
            _sb.Append($"{ingredient.Data.Name},{ingredient.Count}");
        }
        return _sb.ToString();
    }

    void InitRecipe()
    {     
        for(int i = 0; i < _recipeData.RecipeList.Count; i++)
        {
            _resultList.Add(_recipeData.RecipeList[i]);
        }

        _resultList.Sort((s1, s2) => s1.Data.ID.CompareTo(s2.Data.ID));
        for (int i = 1; i < _resultList.Count - 1; i++)
        {
            IngredientInfo temp = new IngredientInfo();
            temp.Data = _resultList[i].Data;
            temp.Count = Util.Random(0, 2);
            _resultList[i] = temp;
        }
    }
}
