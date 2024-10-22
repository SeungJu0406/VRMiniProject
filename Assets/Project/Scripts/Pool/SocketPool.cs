using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketPool : MonoBehaviour
{
    public static SocketPool Instance;

    [SerializeField] BurgerSocket _socketPrefab;
    [SerializeField] int _size = 5;

    Queue<BurgerSocket> _pool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _pool =  new Queue<BurgerSocket>(_size);
        for(int i =  0; i < _size; i++)
        {
            BurgerSocket instance = Instantiate(_socketPrefab);
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(transform);
            _pool.Enqueue(instance); 
        } 
    }

    public BurgerSocket GetPool(Transform getterTransform)
    {
        if (_pool.Count > 0)
        {
            BurgerSocket instance = _pool.Dequeue();
            instance.transform.position = getterTransform.position;
            instance.transform.rotation = getterTransform.rotation;
            instance.transform.SetParent(getterTransform);
            instance.transform.localScale = Vector3.one;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            BurgerSocket instance = Instantiate(_socketPrefab);
            instance.transform.position = getterTransform.position;
            instance.transform.rotation = getterTransform.rotation;
            instance.transform.SetParent(getterTransform);
            instance.transform.localScale = Vector3.one;
            return instance;
        }
    }

    public void ReturnPool(BurgerSocket instance)
    {
        instance.transform.SetParent(transform);
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }
}
