using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private string _firstSceneToLoad = "MainMenu";
    [SerializeField] private MonoBehaviour[] _Singletons = null;

    private void Start()
    {
        foreach (var manager in _Singletons)
        {
            if (manager is IManager m)
            {
                m.Init();
            }
            else
            {
                Debug.LogWarning($"{manager.name} n'implémente pas IManager");
            }
        }
        SceneManager.LoadScene( _firstSceneToLoad );
    }
}
