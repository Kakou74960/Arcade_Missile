using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private string _currentLevel;
    [SerializeField] private string _nextLevel;
    [SerializeField] private string _mainMenu;
    [SerializeField] private TMP_Text _textMissile;
    [SerializeField] private TMP_Text _textTime;

    private void Awake()
    {
        LevelManager.Instance.LevelMenu = this;
    }

    private void Start()
    {
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
        SceneManager.LoadScene(_mainMenu);
    }

    public void Retry()
    {
        SceneManager.LoadScene(_currentLevel);
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
