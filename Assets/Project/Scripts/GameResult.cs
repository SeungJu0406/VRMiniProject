using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : Result
{
    protected override void InitRecipe()
    {
        _resultList.Clear();
        _resultUI.ClearResultText();
        for (int i = 0; i < _recipeData.RecipeList.Count; i++)
        {
            _resultList.Add(_recipeData.RecipeList[i]);
        }

        _resultList.Sort((s1, s2) => s1.Data.ID.CompareTo(s2.Data.ID));
        for (int i = 1; i < _resultList.Count - 1; i++)
        {
            int randomCount = Util.Random(0, 2);
            if (randomCount > 0)
            {
                IngredientInfo temp = new IngredientInfo();
                temp.Data = _resultList[i].Data;
                temp.Count = randomCount;
                _resultList[i] = temp;
                _resultUI.UpdateResultText(_resultList[i]);
            }
            else
            {
                _resultList.RemoveAt(i);
                i--;
            }
        }
    }
    protected override void ProcessSucess()
    {
        base.ProcessSucess();
        // 성공 접시 삭제 연산
        DeleteSuccessPlate();

        InitRecipe();
    }
    void DeleteSuccessPlate()
    {
        // 탑부터 삭제
        while (_resultPlate.TopIngredient != null)
        {
            Ingredient curTop = _resultPlate.TopIngredient;
            if (_resultPlate.TopIngredient.Parent != null)
            {
                _resultPlate.TopIngredient = _resultPlate.TopIngredient.Parent;
            }
            Destroy(curTop.gameObject);
        }
        Destroy(_resultPlate.gameObject);
    }
}
