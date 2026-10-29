using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenu : MenuBase
{
    [Header("Level Selection Menu Settings")]
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameObject _selectedInMainMenu;

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private string _overviewName;
    [SerializeField] private Animator[] _levelOverviews;
    [Header("Scroll Settings")]
    [SerializeField] private RectTransform _layoutTransform;
    [SerializeField] private float _scrollStrength = 0.5f;

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

        if (_selectedButton != null)
            CenterElement(_selectedButton.GetComponent<RectTransform>());
    }

    private void CenterElement(RectTransform elementTransform)
    {
        if (elementTransform != null)
        {
            Vector3 relativeCenter = Vector3.zero;
            relativeCenter.x = _layoutTransform.sizeDelta.x/2 - elementTransform.anchoredPosition.x;
            relativeCenter.y = _layoutTransform.anchoredPosition.y;
            _layoutTransform.anchoredPosition = Vector3.Lerp(_layoutTransform.anchoredPosition, relativeCenter, _scrollStrength);
        }
    }

    public void LoadLevel(string level)
    {
        //AudioManager.Instance.PlaySFXOneShot("CLICK");
        SceneManager.LoadScene(level);
    }

    public void ReturnMainMenu()
    {
        TransitionSlide(Direction.RIGHT, true);
        _mainMenu.gameObject.SetActive(true);
        _mainMenu.TransitionSlide(Direction.LEFT, false);

        EventSystem.current.SetSelectedGameObject(_selectedInMainMenu);
        //this.gameObject.SetActive(false);
    }
}