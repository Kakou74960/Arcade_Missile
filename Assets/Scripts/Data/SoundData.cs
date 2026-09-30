using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    #region Fields
    [SerializeField] private string _iD = "EMPTY";
    [SerializeField] private AudioClip _audioClip;
    [Range(0, 1)][SerializeField] private float _volume = 1.0f;
    #endregion Fields

    #region Properties
    public string ID => _iD;
    public AudioClip AudioClip => _audioClip;
    public float Volume => _volume;
    #endregion Properties
}