using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] public IngredientData data;

    [SerializeField] public Transform stackPivot;

    [SerializeField] public Ingredient parent;
    [SerializeField] public Ingredient child;
}
