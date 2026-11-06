using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MenuBase
{
    [Header("Main Menu Settings")]
    [SerializeField] private string _musicMainMenu;
    [SerializeField] private LevelSelectionMenu _levelSelectionMenu;
    [SerializeField] private string _playLevel = "Level 1";

    private void Awake()
    {
        UiManager.Instance.MainMenu = this;
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_musicMainMenu);
        //DontDestroyOnLoad(this);
    }
    private void Update()
    {
        /*if (Application.isEditor && )
        {
            SceneManager.LoadScene("Game");
        }*/
    }

    public void Play()
    {
        //AudioManager.Instance.PlaySFXOneShot("CLICK");
        TransitionSlide(Direction.UP, true);
        StartCoroutine(LoadScene(_playLevel));
    }

    public void LevelSelectionMenu()
    {
        TransitionSlide(Direction.LEFT, true);
        StartCoroutine(MenuTransition(_levelSelectionMenu, Direction.RIGHT, false));

        EventSystem.current.SetSelectedGameObject(UiManager.Instance.LevelSelectionMenu.DefaultSelected);
        //this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}