using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class RecipeData :ScriptableObject
{
    public List<IngredientInfo> RecipeList;
}
