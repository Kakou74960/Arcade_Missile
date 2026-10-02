using NUnit.Framework;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>, IManager
{
    [SerializeField] private bool _levelFinished = false;

    private LevelMenu _levelMenu;
    private int _missileUsed = 0;
    private float _timeSpent = 0;
    private int _objective = 0;

    #region Properties
    public bool LevelFinished
    {
        get => _levelFinished;
        set => _levelFinished = value;
    }

    public LevelMenu LevelMenu
    {
        get => _levelMenu;
        set => _levelMenu = value;
    }

    public int MissileUsed
    {
        get => _missileUsed;
        set => _missileUsed = value;
    }

    public float TimeSpent
    {
        get => _timeSpent;
        set => _timeSpent = value;
    }

    public int Objective
    {
        get => _objective;
        set => _objective = value;
    }

    #endregion Properties

    private void Update()
    {
        if (!_levelFinished)
            CountTimeLevel();
        
        if (Objective == 0 && LevelMenu != null)
        {
            _levelFinished = true;
            LevelMenu.gameObject.SetActive(true);
            LevelMenu.ShowScore();
        }
    }

    private void OnLevelWasLoaded()
    {
        TimeSpent = 0;
        MissileUsed = 0;
    }

    private void CountTimeLevel()
    {
        TimeSpent += Time.deltaTime;
    }
}
