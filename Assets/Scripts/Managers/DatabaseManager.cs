using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : Singleton<DatabaseManager>, IManager
{
    #region Fields
    [SerializeField] private string _saveFilesPath;
    [SerializeField] private string _levelsName;
    [SerializeField] private SoundData[] _soundsArray = null;
    [SerializeField] private SoundData[] _musicsArray = null;

    private Dictionary<string, SoundData> _sounds = null;
    private Dictionary<string, SoundData> _musics = null;
    private Dictionary<string, List<HighscoreStruct>> _dicHighscores = new();

    private string _savePath;
    private int _indexToInsert = -1;
    #endregion Fields

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, _saveFilesPath);
    }

    private void Start()
    {
        RecoverHighscoresFiles();
    }

    public override void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _sounds = new Dictionary<string, SoundData>();
        _musics = new Dictionary<string, SoundData>();

        for (int i = 0; i < _soundsArray.Length; i++)
        {
            _sounds.Add(_soundsArray[i].ID, _soundsArray[i]);
        }
        for (int i = 0; i < _musicsArray.Length; i++)
        {
            _musics.Add(_musicsArray[i].ID, _musicsArray[i]);
        }
    }

    public SoundData GetSFXByID(string ID)
    {
        return _sounds[ID];
    }
    
    public SoundData GetMusicByID(string ID)
    {
        return _musics[ID];
    }

    public List<HighscoreStruct> GetHighscores(string sceneName)
    {
        if (_dicHighscores.ContainsKey(sceneName))
            return _dicHighscores.GetValueOrDefault(sceneName);
        return new();
    }

    #region Save
    private void RecoverHighscoresFiles()
    {
        int levelIndex = SceneManager.sceneCountInBuildSettings - 3;

        // Pour chaque scène dans build Settings
        for (int i = 1; i <= levelIndex; i++)
        {
            // Le chemin pour retrouver les fichiers de sauvegardes
            string recoverPath = Path.Combine(_savePath, _levelsName + i) + ".json";

            if (File.Exists(recoverPath))
            {
                // commande Json pour lire le texte
                string json = File.ReadAllText(recoverPath);
                // Convertit les listes du fichier en objets unity
                _dicHighscores.Add(_levelsName + i, JsonUtility.FromJson<SaveData>(json).saveHighscores);
            }
            else
            {
                Debug.Log("fichier introuvable : " + recoverPath);
                _dicHighscores.Add(_levelsName + i, new List<HighscoreStruct>());
            }
        }
    }

    public void SaveHighscoresFiles(List<HighscoreStruct> listData, string sceneName)
    {
        SaveData save = new()
        {
            saveHighscores = listData
        };
        // commande Json pour écrire le texte
        string json = JsonUtility.ToJson(save);
        // Écrit un fichier contenant les listes en Json
        File.WriteAllText(Path.Combine(_savePath, sceneName) + ".json", json);
    }

    public void SaveHighscore(HighscoreStruct data, string sceneName)
    {
        if (_indexToInsert != -1)
            _dicHighscores.GetValueOrDefault(sceneName).Insert(_indexToInsert, data);
        else if (LevelManager.Instance.ListHighscores.Count <= 9)
            _dicHighscores.GetValueOrDefault(sceneName).Add(data);

        LevelManager.Instance.ListHighscores = _dicHighscores.GetValueOrDefault(sceneName);
    }

    public bool CheckHighscore(HighscoreStruct data, string sceneName)
    {
        _indexToInsert = -1;
        bool newHighscore = false;

        if (_dicHighscores.ContainsKey(sceneName))
        {
            List<HighscoreStruct> listHighscores = _dicHighscores.GetValueOrDefault(sceneName);

            // Compare si le Highscore est dans le top 10
            foreach (HighscoreStruct highscore in listHighscores)
            {
                if (data.rocket <  highscore.rocket)
                {
                    _indexToInsert = listHighscores.IndexOf(highscore);
                    newHighscore = true;
                    break;
                }
                else if (data.rocket == highscore.rocket)
                {
                    if (data.time < highscore.time)
                    {
                        _indexToInsert = listHighscores.IndexOf(highscore);
                        newHighscore = true;
                        break;
                        // Ajouter un feedback de nouveau Highscore
                    }
                }
            }

            // Retire si plus de 10 Highscores
            if (listHighscores.Count > 10)
            {
                listHighscores.RemoveAt(10);
            }
            else if (listHighscores.Count < 10)
                newHighscore = true;
        }
        else
            Debug.LogError("No Level file found of name : " + sceneName);

        return newHighscore;
    }
    #endregion Save
}
