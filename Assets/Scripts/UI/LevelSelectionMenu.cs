using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _defaultSelected;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _selectecInMainMenu;

    private void Start()
    {
        //AudioManager.Instance.PlayMusic("MAIN_MENU_MUSIC");
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            EventSystem.current.SetSelectedGameObject(_defaultSelected);
        }
    }

    public void LoadLevel(string level)
    {
        //AudioManager.Instance.PlaySFXOneShot("CLICK");
        SceneManager.LoadScene(level);
    }

    public void ReturnMainMenu()
    {
        _mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_selectecInMainMenu);
        this.gameObject.SetActive(false);
    }
}