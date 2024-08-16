using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
public class SceneLoader : MonoBehaviour
{
    [ScenePath]
    public string Scene;

    public bool LoadOnStart;

    void Start()
    {
        if (LoadOnStart)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        //path not name?
        SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
    }
}
