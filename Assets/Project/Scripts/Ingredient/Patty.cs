using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patty : Ingredient
{
    [SerializeField] public float MaxCookTime;
    [SerializeField] public float CurCookTime;

    [SerializeField] public Patty NextPatty;
}
