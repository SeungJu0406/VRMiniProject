using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPool : MonoBehaviour
{
    public static StackPool Instance;

    [SerializeField] BurgerStack stackPrefab;
    [SerializeField] int size = 5;

    Queue<BurgerStack> pool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        pool =  new Queue<BurgerStack>(size);
        for(int i =  0; i < size; i++)
        {
            BurgerStack instance = Instantiate(stackPrefab);
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(transform);
            pool.Enqueue(instance); 
        } 
    }

    public BurgerStack GetPool(Transform getterTransform)
    {
        if(pool.Count > 0)
        {
            BurgerStack instance = pool.Dequeue();
            instance.transform.position = getterTransform.position;
            instance.transform.rotation = getterTransform.rotation;
            instance.transform.SetParent(getterTransform);
            instance.transform.localScale = Vector3.one;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            BurgerStack instance = Instantiate(stackPrefab);
            instance.transform.position = getterTransform.position;
            instance.transform.rotation = getterTransform.rotation;
            instance.transform.SetParent(getterTransform);
            instance.transform.localScale = Vector3.one;
            return instance;
        }
    }

    public void ReturnPool(BurgerStack instance)
    {
        instance.transform.SetParent(transform);
        instance.gameObject.SetActive(false);
        pool.Enqueue(instance);
    }
}
