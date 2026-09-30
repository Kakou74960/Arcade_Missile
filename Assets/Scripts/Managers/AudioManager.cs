using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>, IManager
{

    [SerializeField] private AudioSource _SFXSource = null;
    [SerializeField] private AudioSource _musicSource = null;

    public void PlaySFXOneShot(string iD)
    {
        SoundData sound = DatabaseManager.Instance.GetSFXByID(iD);
        _SFXSource.PlayOneShot(sound.AudioClip, sound.Volume);
    }

    public void PlayMusic(string iD)
    {
        SoundData music = DatabaseManager.Instance.GetMusicByID(iD);
        _musicSource.Stop();
        _musicSource.clip = music.AudioClip;
        _musicSource.volume = music.Volume;
        _musicSource.Play();
    }
}
