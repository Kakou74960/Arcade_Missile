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
        _savePath = Application.persistentDataPath + _saveFilesPath;
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

    #region Save
    private void RecoverHighscoresFiles()
    {
        int buildIndex = SceneManager.sceneCountInBuildSettings;

        // Pour chaque scène dans build Settings
        for (int i = 0; i < buildIndex; i++)
        {
            // Le chemin pour retrouver les fichiers de sauvegardes
            string recoverPath = _savePath + SceneManager.GetSceneByBuildIndex(i).name + ".json";

            if (File.Exists(recoverPath))
            {
                // commande Json pour lire le texte
                string json = File.ReadAllText(recoverPath);
                // Convertit les listes du fichier en objets unity
                _dicHighscores.Add(SceneManager.GetSceneByBuildIndex(i).name, JsonUtility.FromJson<List<HighscoreStruct>>(json));
            }
            else
                Debug.Log("fichier introuvable " + SceneManager.GetSceneByBuildIndex(i).name);
        }
    }

    public void SaveHighscoresFiles(List<HighscoreStruct> listData, string sceneName)
    {
        // commande Json pour écrire le texte
        string json = JsonUtility.ToJson(listData , true);
        // Écrit un fichier contenant les listes en Json
        File.WriteAllText(_savePath + sceneName + ".json", json);
    }

    public void SaveHighscore(HighscoreStruct data, string sceneName)
    {
        _dicHighscores.GetValueOrDefault(sceneName)[_indexToInsert] = data;
    }

    public void CheckHighscore(HighscoreStruct data, string sceneName)
    {
        if (_dicHighscores.ContainsKey(sceneName))
        {
            List<HighscoreStruct> listHighscores = _dicHighscores.GetValueOrDefault(sceneName);

            // Compare si le Highscore est dans le top 10
            foreach (HighscoreStruct highscore in listHighscores)
            {
                if (data.rocket <= highscore.rocket)
                {
                    if (data.time < highscore.time)
                    {
                        _indexToInsert = listHighscores.IndexOf(highscore);
                        //listHighscores.Insert(_indexToInsert, data);

                        // Ajouter un feedback de nouveau Highscore
                    }
                }
            }

            // Retire si plus de 10 Highscores
            if (listHighscores.Count > 10)
            {
                listHighscores.RemoveAt(10);
            }
        }
        else
            Debug.LogError("No Level file found of name : " +  sceneName);
    }
    #endregion Save
}
