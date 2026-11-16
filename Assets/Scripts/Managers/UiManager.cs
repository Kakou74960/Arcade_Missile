using UnityEngine;

public class UiManager : Singleton<UiManager>, IManager
{
    #region Fields
    private TitleMenu _titleMenu;
    private MainMenu _mainMenu;
    private LevelSelectionMenu _levelSelectionMenu;
    private bool _onTitleMenu = true;
    private bool _loadToMainMenu = false;
    private bool _loadToLevelSelection = false;
    #endregion Fields

    #region Properties
    public MainMenu MainMenu
    {
        get => _mainMenu;
        set
        {
            _mainMenu = value;
            if (LoadToMainMenu)
            {
                _mainMenu.TransitionSlide(MenuBase.Direction.UP, false);
                _titleMenu.gameObject.SetActive(false);
            }
            else
                _mainMenu.gameObject.SetActive(false);
        }
    }

    public LevelSelectionMenu LevelSelectionMenu
    {
        get => _levelSelectionMenu;
        set
        {
            _levelSelectionMenu = value;
            if (LoadToLevelSelection)
            {
                _levelSelectionMenu.gameObject.SetActive(true);
                _levelSelectionMenu.TransitionSlide(MenuBase.Direction.UP, false);
                _titleMenu.gameObject.SetActive(false);
            }
            else
                _levelSelectionMenu.gameObject.SetActive(false);
        }
    }

    public TitleMenu TitleMenu
    {
        get { return _titleMenu; }
        set { _titleMenu = value; }
    }

    public bool LoadToMainMenu
    { 
        get => _loadToMainMenu;
        set
        {
            ResetLoadTo();
            _loadToMainMenu = value;
        }
    }

    public bool LoadToLevelSelection
    { 
        get => _loadToLevelSelection;
        set
        {
            ResetLoadTo();
            _loadToLevelSelection = value;
        }
    }

    public bool OnTitleMenu
    {
        get => _onTitleMenu;
        set {_onTitleMenu = value;}
    }
    #endregion Properties

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetLoadTo()
    {
        _loadToLevelSelection = false;
        _loadToMainMenu = false;
    }
}
