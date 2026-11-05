using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MenuBase
{
    [Header("Level Menu Settings")]
    [SerializeField] private float _slideStrength = 1;
    [Header("Main Menu Button")]
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private string _mainMenuName;
    [Header("Retry Button")]
    [SerializeField] private Button _retryButton;
    [SerializeField] private string _retryLevelName;
    [Header("Next Level Button")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private string _nextLevelName;
    [Header("Highscore")]
    [SerializeField] private TMP_Text _textMissile;
    [SerializeField] private TMP_Text _textTime;
    [SerializeField] private TMP_Text _newHighscoreText;
    [SerializeField] private TMP_Text _nicknameText;
    [SerializeField] private float _blinkTime = 0.5f;
    [SerializeField] private char _blinkChar = '_';

    private bool _goToLevels = false;
    private bool _goNextLevel = false;
    private HighscoreStruct _currentScore = new();
    private char[] _nick = new char[3];
    private int _nickIndex = 0;
    private float _currentBlink = 0;
    private bool _toggleBlink = true;

    private void Awake()
    {
        LevelManager.Instance.LevelMenu = this;
    }

    private void Start()
    {
        if(_mainMenuName == string.Empty)
        {
            _mainMenuButton.gameObject.SetActive(false);
        }

        if (_retryLevelName == string.Empty)
        {
            _retryButton.gameObject.SetActive(false);
        }

        if(_nextLevelName == string.Empty)
        {
            _nextButton.gameObject.SetActive(false);
        }

        for (int c = 0; c < _nick.Length; c++)
        {
            _nick[c] = 'A';
        }

        _nicknameText.alpha = 0;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_goToLevels)
        {
            Camera.main.transform.position += Vector3.up * _slideStrength * Time.deltaTime;
        }
        if (_goNextLevel)
        {
            Camera.main.transform.position += Vector3.right * _slideStrength * 1.6f * Time.deltaTime;
        }

        if (LevelManager.Instance.ChangeNickname)
        {
            ScoreBoardControls();
        }
    }

    private void ScoreBoardControls()
    {
        if (InputManager.Instance.NextLetter.triggered)
        {
            _nick[_nickIndex]++;
            if (_nick[_nickIndex] == '[')
            {
                _nick[_nickIndex] = '0';
            }
            if (_nick[_nickIndex] == ':')
            {
                _nick[_nickIndex] = 'A';
            }
        }
        if (InputManager.Instance.PreviousLetter.triggered)
        {
            _nick[_nickIndex]--;
            if (_nick[_nickIndex] == '/')
            {
                _nick[_nickIndex] = 'Z';
            }
            if (_nick[_nickIndex] == '@')
            {
                _nick[_nickIndex] = '9';
            }
        }
        if (InputManager.Instance.SelectRight.triggered)
        {
            _nickIndex++;
            if (_nickIndex >= _nick.Length)
                _nickIndex = 0;
        }
        if (InputManager.Instance.SelectLeft.triggered)
        {
            _nickIndex--;
            if (_nickIndex < 0)
                _nickIndex = _nick.Length - 1;
        }
        if (InputManager.Instance.Submit.triggered)
        {
            LevelManager.Instance.ChangeNickname = false;
            _currentScore.name = _nick.ArrayToString();
            DatabaseManager.Instance.SaveHighscore(_currentScore, SceneManager.GetActiveScene().name);
            DatabaseManager.Instance.SaveHighscoresFiles(LevelManager.Instance.ListHighscores, SceneManager.GetActiveScene().name);
            _baseMenuAnimator.SetTrigger("Boutons");
        }
        
        _nicknameText.text = BlinkLetter().ArrayToString();
    }

    private char[] BlinkLetter()
    {
        if (_currentBlink >= _blinkTime)
        {
            _currentBlink = 0;
            _toggleBlink = !_toggleBlink;
        }

        char[] charToString = new char[_nick.Length];
        for (int c = 0; c < _nick.Length; c++)
        {
            if (c == _nickIndex && _toggleBlink && LevelManager.Instance.ChangeNickname)
                charToString[c] = _blinkChar;
            else
                charToString[c] = _nick[c];
        }
        _currentBlink += Time.deltaTime;
        return charToString;
    }

    public void ShowScore()
    {
        _textMissile.text = LevelManager.Instance.MissileUsed.ToString();
        
        _currentScore.rocket = LevelManager.Instance.MissileUsed;
        _currentScore.time = LevelManager.Instance.TimeSpent;

        float minutes = LevelManager.Instance.TimeSpent / 60;
        float seconds = LevelManager.Instance.TimeSpent % 60;
        float milliseconds = LevelManager.Instance.TimeSpent - (int)LevelManager.Instance.TimeSpent;
        _textTime.text = string.Format("{0:00}:{1:00}{2:.000}", minutes, seconds, milliseconds);

        if (DatabaseManager.Instance.CheckHighscore(_currentScore, SceneManager.GetActiveScene().name))
        {
            NewBestHighscore();
        }
        else
            _baseMenuAnimator.SetTrigger("Boutons");
    }

    public void MainMenu()
    {
        _goToLevels = true;
        UiManager.Instance.LoadToMainMenu = true;
        TransitionSlide(Direction.DOWN, true);
        StartCoroutine(LoadMainMenu());
    }

    public void Retry()
    {
        SceneManager.LoadScene(_retryLevelName);
    }

    public void LevelSelector()
    {
        _goToLevels = true;
        UiManager.Instance.LoadToLevelSelection = true;
        TransitionSlide(Direction.DOWN, true);
        StartCoroutine(LoadMainMenu());
    }

    public void NextLevel()
    {
        _goNextLevel = true;
        TransitionSlide(Direction.LEFT, true);
        StartCoroutine(LoadScene(_nextLevelName));

    }

    public void Highscore()
    {
        LevelManager.Instance.HighscoreMenu.gameObject.SetActive(true);
        LevelManager.Instance.HighscoreMenu.UpdateScoreBoard();
        EventSystem.current.SetSelectedGameObject(LevelManager.Instance.HighscoreMenu.DefaultSelected);
        this.gameObject.SetActive(false);
    }

    private void NewBestHighscore()
    {
        _baseMenuAnimator.SetTrigger("NewScore");
        _nicknameText.alpha = 1;
        LevelManager.Instance.ChangeNickname = true;
        EventSystem.current.SetSelectedGameObject(null);
        //DatabaseManager.Instance.SaveHighscore();
    }
}
