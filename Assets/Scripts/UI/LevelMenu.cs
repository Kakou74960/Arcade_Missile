using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private string _currentLevel;
    [SerializeField] private string _nextLevel;
    [SerializeField] private string _mainMenu;

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
