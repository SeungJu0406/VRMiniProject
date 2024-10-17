using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Result : MonoBehaviour
{
    [Header("기본 레시피")]
    [SerializeField] RecipeData recipeData;
    [Header("햄버거 빵 체크")]
    [SerializeField] IngredientData bottomData;
    [SerializeField] IngredientData topData;


    [SerializeField] List<IngredientInfo> resultList = new List<IngredientInfo>(10);
    Plate resultPlate;

    StringBuilder sb = new StringBuilder();

    private void Start()
    {
        InitRecipe();
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        resultPlate = args.interactableObject.transform.GetComponent<Plate>();
        ProcessResult();
    }

    void ProcessResult()
    {
        if (resultPlate.BottomIngredient.data == bottomData && resultPlate.TopIngredient.data == topData )
        {
            string plate = resultPlate.GetValueToString();
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
        resultList.Sort((s1, s2) => s1.data.ID.CompareTo(s2.data.ID));
        sb.Clear();
        foreach (IngredientInfo ingredient in resultList)
        {
            sb.Append($"{ingredient.data.Name},{ingredient.count}");
        }
        return sb.ToString();
    }

    void InitRecipe()
    {
        resultList = recipeData.recipeList;
        for (int i = 1; i < resultList.Count - 1; i++)
        {
            IngredientInfo temp = new IngredientInfo();
            temp.data = resultList[i].data;
            temp.count = Util.Random(0, 2);
            resultList[i] = temp;
        }
    }
}
