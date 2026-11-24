using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class TitleMenu : MenuBase
{
    [SerializeField] private TMP_Text _blinkText;
    [SerializeField] private float _blink = 1;

    private float _currentBlink;

    private void Awake()
    {
        UiManager.Instance.TitleMenu = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.Autodestruct.performed += TitleAction;
    }

    // Update is called once per frame
    void Update()
    {
        if (UiManager.Instance.OnTitleMenu)
        {
            if (InputManager.Instance.Boost != 0 || InputManager.Instance.Brake != 0)
                StartGame();
            Blink();
        }
    }

    private void StartGame()
    {
        _blinkText.enabled = false;
        _currentBlink = 0;

        InputManager.Instance.Autodestruct.performed -= TitleAction;
        UiManager.Instance.OnTitleMenu = false;
        TransitionSlide(Direction.DOWN, true);
        StartCoroutine(MenuTransition(UiManager.Instance.MainMenu, Direction.UP, false,UiManager.Instance.MainMenu.DefaultSelected));
    }

    private void TitleAction (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartGame();
        }
    }

    private void Blink()
    {
        if (_currentBlink >= _blink)
        {
            _blinkText.enabled = !_blinkText.enabled;
            _currentBlink = 0;
        }
        _currentBlink += Time.deltaTime;
    }
}
