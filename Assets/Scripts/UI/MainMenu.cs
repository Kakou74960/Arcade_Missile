using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MenuBase
{
    [Header("Main Menu Settings")]
    [SerializeField] private string _musicMainMenu;

    [SerializeField] private LevelSelectionMenu _levelSelectionMenu;
    [SerializeField] private GameObject _selectedInLevelSelection;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_musicMainMenu);
    }
    private void Update()
    {

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            EventSystem.current.SetSelectedGameObject(_defaultSelected);
        }
    }

    public void Play()
    {
        //AudioManager.Instance.PlaySFXOneShot("CLICK");
        SceneManager.LoadScene("Game");
    }

    public void LevelSelectionMenu()
    {
        TransitionSlide(Direction.LEFT, true);
        _levelSelectionMenu.gameObject.SetActive(true);
        _levelSelectionMenu.TransitionSlide(Direction.RIGHT, false);

        EventSystem.current.SetSelectedGameObject(_selectedInLevelSelection);
        //this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}