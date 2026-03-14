using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBootStrapper : MonoBehaviour
{
    void Awake()
    {
        if (!SceneManager.GetSceneByName("Managers").isLoaded)
        {
            SceneManager.LoadSceneAsync("Managers", LoadSceneMode.Additive);
        }

        if (!SceneManager.GetSceneByName("UI").isLoaded)
        {
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }

        if (!SceneManager.GetSceneByName("Stage").isLoaded)
        {
            SceneManager.LoadSceneAsync("Stage", LoadSceneMode.Additive);
        }

        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        }

        if (!SceneManager.GetSceneByName("Camera").isLoaded)
        {
            SceneManager.LoadSceneAsync("Camera", LoadSceneMode.Additive);
        }

        if (!SceneManager.GetSceneByName("Pickups").isLoaded)
        {
            SceneManager.LoadSceneAsync("Pickups", LoadSceneMode.Additive);
        }
    }
}
