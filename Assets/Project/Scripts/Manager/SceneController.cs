using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [Header("ó�� �߰� �ε��� ��")]
    [SerializeField] SceneField[] _startScenes;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        LoadStartScene();
    }

    void LoadStartScene()
    {
        foreach (SceneField scene in _startScenes)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
    }
}
