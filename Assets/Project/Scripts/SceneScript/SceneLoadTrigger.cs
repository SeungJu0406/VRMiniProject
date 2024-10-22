using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _SceneToUnload;

    GameObject _sceneChanger;

    private void Awake()
    {
        _sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _sceneChanger.tag)
        {
            LoadScene();
            UnLoadScene();
        }
    }

    void LoadScene()
    {
        for (int i = 0; i < _sceneToLoad.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _sceneToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_sceneToLoad[i], LoadSceneMode.Additive);
            }
        }
    }
    void UnLoadScene()
    {
        for(int i = 0; i < _SceneToUnload.Length; i++)
        {
            for(int j = 0; j< SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if(loadedScene.name == _SceneToUnload[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(_SceneToUnload[i]);
                }
            }
        }
    }
}
