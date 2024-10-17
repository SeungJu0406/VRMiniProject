using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayManager : MonoBehaviour
{
    public static DelayManager Instance;

    Dictionary<float, WaitForSeconds> Dic = new Dictionary<float, WaitForSeconds>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Dic.Add(0.2f, new WaitForSeconds(0.2f));
    }
    
    public WaitForSeconds Get(float time)
    {
        return Dic[time];
    }
}
