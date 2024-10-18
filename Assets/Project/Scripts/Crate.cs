using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Crate : MonoBehaviour
{
    [SerializeField] Ingredient _ingredientPrefab;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Vector3 testPos = new Vector3(-0.2f, 0.7f, 1);


        Ingredient instance = Instantiate(_ingredientPrefab, testPos ,Quaternion.identity);

        // 생성한 재료가 자동으로 손에 잡히는 로직 구현
    }
}
