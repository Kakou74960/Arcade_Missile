using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [Header("Main Menu Button")]
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private string _mainMenuLevel;
    [Header("Retry Button")]
    [SerializeField] private Button _retryButton;
    [SerializeField] private string _retryLevel;
    [Header("Next Level Button")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private string _nextLevel;
    [Header("Highscore")]
    [SerializeField] private TMP_Text _textMissile;
    [SerializeField] private TMP_Text _textTime;

    private void Awake()
    {
        LevelManager.Instance.LevelMenu = this;
    }

    private void Start()
    {
        if(_mainMenuLevel == string.Empty)
        {
            _mainMenuButton.gameObject.SetActive(false);
        }

        if (_retryLevel == string.Empty)
        {
            _retryButton.gameObject.SetActive(false);
        }

        if(_nextLevel == string.Empty)
        {
            _nextButton.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void ShowScore()
    {
        _textMissile.text = LevelManager.Instance.MissileUsed.ToString();

        float minutes = LevelManager.Instance.TimeSpent / 60;
        float seconds = LevelManager.Instance.TimeSpent % 60;
        _textTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuLevel);
    }

    public void Retry()
    {
        SceneManager.LoadScene(_retryLevel);
    }

    public void LevelSelector()
    {

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_nextLevel);
    }

    public void Highscore()
    {

    }
}
