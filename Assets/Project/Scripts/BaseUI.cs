using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    private Dictionary<string, GameObject> _gameObjectDic;
    private Dictionary<(string, System.Type), Component> _componentDic;

    protected virtual void Awake()
    {
        Bind();
    }

    protected void Bind()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        _gameObjectDic = new Dictionary<string, GameObject>(transforms.Length << 2);
        _componentDic = new Dictionary<(string, System.Type), Component>(transforms.Length << 2);
        foreach (Transform t in transforms)
        {
            _gameObjectDic.Add(t.name, t.gameObject);
        }
    }

    protected GameObject GetUI(in string name)
    {
        _gameObjectDic.TryGetValue(name, out GameObject gameObject);
        return gameObject;
    }

    protected T GetUI<T>(in string name) where T : Component
    {
        (string, System.Type) key = (name, typeof(T));

        // ������Ʈ ��ųʸ��� �ִ°�?
        _componentDic.TryGetValue(key, out Component component);
        if (component != null)
        {
            return component as T;
        }

        // �ش� ���� ������Ʈ�� ��ųʸ��� �ִ°�?
        _gameObjectDic.TryGetValue(name, out GameObject gameObject);
        if (gameObject == null)
        {
            return null;
        }

        // ã�� ���� ������Ʈ�� ���ϴ� ������Ʈ�� �ִ°�?
        component = gameObject.GetComponent<T>();
        if (component == null)
        {
            return null;
        }
        // ������ ��ųʸ��� �߰��ؼ� ��ȯ
        else
        {
            _componentDic.Add(key, component);
            return component as T;
        }
    }
}