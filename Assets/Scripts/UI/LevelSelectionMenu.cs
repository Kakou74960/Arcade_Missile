using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _defaultSelected;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _selectedInMainMenu;

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private string _overviewName;
    [SerializeField] private Animator[] _levelOverviews;

    private GameObject _selectedButton;

    private void Start()
    {
        //AudioManager.Instance.PlayMusic("MAIN_MENU_MUSIC");
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != _selectedButton)
        {
            if (_selectedButton != null)
            {
                _selectedButton.transform.Find(_overviewName).GetComponent<Animator>().SetBool("isOpen", false);
                //_selectedButton.GetComponentInChildren<Animator>().SetTrigger("Close");
                _selectedButton = null;
            }

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                if (_levelButtons[i].gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    //_levelOverviews[i].SetTrigger("Open");
                    _levelButtons[i].transform.Find(_overviewName).GetComponent<Animator>().SetBool("isOpen", true);
                    _selectedButton = _levelButtons[i].gameObject;
                }
            }
        }

        /// Si on perd la sélection
        if(EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(_defaultSelected);
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
        EventSystem.current.SetSelectedGameObject(_selectedInMainMenu);
        this.gameObject.SetActive(false);
    }
}