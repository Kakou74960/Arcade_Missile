using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreMenu : MenuBase
{
    [Header("Highscore Menu Settings")]
    [SerializeField] private ScoreUi[] _scores;

    private void Awake()
    {
        LevelManager.Instance.HighscoreMenu = this;
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_defaultSelected);
    }

    private void Update()
    {
        
    }

    public void UpdateScoreBoard()
    {
        for (int scoreIndex = 0; scoreIndex < LevelManager.Instance.ListHighscores.Count; scoreIndex++)
        {
            _scores[scoreIndex]._nameText.text = LevelManager.Instance.ListHighscores[scoreIndex].name;
            _scores[scoreIndex]._rocketText.text = LevelManager.Instance.ListHighscores[scoreIndex].rocket.ToString();

            float minutes = LevelManager.Instance.ListHighscores[scoreIndex].time / 60;
            float seconds = LevelManager.Instance.ListHighscores[scoreIndex].time % 60;
            _scores[scoreIndex]._timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ReturnToLevelMenu()
    {
        LevelManager.Instance.LevelMenu.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(LevelManager.Instance.LevelMenu.DefaultSelected);
        this.gameObject.SetActive(false);
    }
}
