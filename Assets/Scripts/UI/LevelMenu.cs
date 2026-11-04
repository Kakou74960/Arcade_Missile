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
    [SerializeField] private Color _letterColor = Color.white;

    private bool _goToLevels = false;
    private bool _goNextLevel = false;
    private HighscoreStruct _currentScore = new();
    private char[] _nick = new char[3];
    private int _nickIndex = 0;

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

        _nick[0] = 'A';
        _nick[1] = 'A';
        _nick[2] = 'A';

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
            _currentScore.name = _nicknameText.text;
            DatabaseManager.Instance.SaveHighscore(_currentScore, SceneManager.GetActiveScene().name);
            DatabaseManager.Instance.SaveHighscoresFiles(LevelManager.Instance.ListHighscores, SceneManager.GetActiveScene().name);
        }
        _nicknameText.text = _nick.ArrayToString();
    }

    public void ShowScore()
    {
        _textMissile.text = LevelManager.Instance.MissileUsed.ToString();
        
        _currentScore.rocket = LevelManager.Instance.MissileUsed;
        _currentScore.time = LevelManager.Instance.TimeSpent;

        float minutes = LevelManager.Instance.TimeSpent / 60;
        float seconds = LevelManager.Instance.TimeSpent % 60;
        _textTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (DatabaseManager.Instance.CheckHighscore(_currentScore, SceneManager.GetActiveScene().name))
        {
            NewBestHighscore();
        }
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
        LevelManager.Instance.ChangeNickname = true;
        EventSystem.current.SetSelectedGameObject(null);
        //DatabaseManager.Instance.SaveHighscore();
    }
}
