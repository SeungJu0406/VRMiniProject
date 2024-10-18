using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlatesCrate : MonoBehaviour
{
    [SerializeField] Plate _platePrefab;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Vector3 testPos = new Vector3(-0.2f, 0.7f, 1);


        Plate instance = Instantiate(_platePrefab, testPos, Quaternion.identity);

        // 생성한 재료가 자동으로 손에 잡히는 로직 구현
    }
}
