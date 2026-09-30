using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : Singleton<DatabaseManager>, IManager
{
    #region Fields
    [SerializeField] private SoundData[] _soundsArray = null;
    [SerializeField] private SoundData[] _musicsArray = null;

    private Dictionary<string, SoundData> _sounds = null;
    private Dictionary<string, SoundData> _musics = null;
    #endregion Fields
    public override void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _sounds = new Dictionary<string, SoundData>();

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
}
