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

        // 컴포넌트 딕셔너리에 있는가?
        _componentDic.TryGetValue(key, out Component component);
        if (component != null)
        {
            return component as T;
        }

        // 해당 게임 오브젝트가 딕셔너리에 있는가?
        _gameObjectDic.TryGetValue(name, out GameObject gameObject);
        if (gameObject == null)
        {
            return null;
        }

        // 찾은 게임 오브젝트에 원하는 컴포넌트가 있는가?
        component = gameObject.GetComponent<T>();
        if (component == null)
        {
            return null;
        }
        // 있으면 딕셔너리에 추가해서 반환
        else
        {
            _componentDic.Add(key, component);
            return component as T;
        }
    }
}