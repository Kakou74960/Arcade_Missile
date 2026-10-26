using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _defaultSelected;

    [SerializeField] private GameObject _levelSelectionMenu;
    [SerializeField] private GameObject _selectedInLevelSelection;

    private void Start()
    {
        //AudioManager.Instance.PlayMusic("MAIN_MENU_MUSIC");
    }
    private void Update()
    {
        /// Si on perd la sélection
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(_defaultSelected);
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
        _levelSelectionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_selectedInLevelSelection);
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}