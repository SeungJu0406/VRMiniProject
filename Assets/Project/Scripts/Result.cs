using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Result : MonoBehaviour
{
    [Header("기본 레시피")]
    [SerializeField] protected RecipeData _recipeData;

    [Header("햄버거 빵 체크")]
    [SerializeField] protected IngredientData _bottomData;
    [SerializeField] protected IngredientData _topData;

    [Header("주문 UI")]
    [SerializeField] protected ResultUI _resultUI;

    [SerializeField] protected List<IngredientInfo> _resultList = new List<IngredientInfo>(10);
    protected Plate _resultPlate;

    protected StringBuilder _sb = new StringBuilder();

    protected virtual void Awake()
    {
        _resultUI = GetComponent<ResultUI>();
    }

    protected virtual void Start()
    {
        InitRecipe();
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        _resultPlate = args.interactableObject.transform.GetComponent<Plate>();
        ProcessResult();
    }

    protected void ProcessResult()
    {
        if (_resultPlate.BottomIngredient?.Data == _bottomData && _resultPlate.TopIngredient?.Data == _topData )
        {
            string plate = _resultPlate.GetValueToString();
            string result = GetValueToString();
            if (plate.Equals(result))
            {
                ProcessSucess();
                return;
            }
        }
        ProcessFailed();
    }

    protected string GetValueToString()
    {
        _resultList.Sort((s1, s2) => s1.Data.ID.CompareTo(s2.Data.ID));
        _sb.Clear();
        foreach (IngredientInfo ingredient in _resultList)
        {
            _sb.Append($"{ingredient.Data.Name},{ingredient.Count}");
        }
        return _sb.ToString();
    }

    protected virtual void InitRecipe() { }

    protected virtual void ProcessSucess()
    {

        _resultUI.UpdateSuccessText();
    }
    protected void ProcessFailed()
    {
        _resultUI.UpdateFailText();
    }


}
