using System;
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
        
    }

    private void Update()
    {
        
    }

    public void ReturnToLevelMenu()
    {

    }
}
