using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPool : MonoBehaviour
{
    public static StackPool Instance;

    [SerializeField] BurgerStack _stackPrefab;
    [SerializeField] int _size = 5;

    Queue<BurgerStack> _pool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _pool =  new Queue<BurgerStack>(_size);
        for(int i =  0; i < _size; i++)
        {
            BurgerStack instance = Instantiate(_stackPrefab);
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(transform);
            _pool.Enqueue(instance); 
        } 
    }

    public BurgerStack GetPool(Transform getterTransform)
    {
        if(_pool.Count > 0)
        {
            BurgerStack instance = _pool.Dequeue();
            instance.transform.position = getterTransform.position;
            instance.transform.rotation = getterTransform.rotation;
            instance.transform.SetParent(getterTransform);
            instance.transform.localScale = Vector3.one;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            BurgerStack instance = Instantiate(_stackPrefab);
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
        _pool.Enqueue(instance);
    }
}
