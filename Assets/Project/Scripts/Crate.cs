using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Crate : MonoBehaviour
{
    [SerializeField] Ingredient _ingredientPrefab;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Ingredient instance = Instantiate(_ingredientPrefab);
    }
}
