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
    [SerializeField] private bool _nothing;

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

    public void ReturnToLevelMenu()
    {
        LevelManager.Instance.LevelMenu.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(LevelManager.Instance.LevelMenu.DefaultSelected);
        this.gameObject.SetActive(false);
    }
}
