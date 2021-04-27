using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
    public static GameMusicPlayer Current;

    [SerializeField]
    private string _mainmenuMusicID;

    [SerializeField]
    private string _introMusicID = "event:/IntroMusic";

    [SerializeField]
    private string _gameplayMusicID ="event:/GamePlayMusic";

    [SerializeField]
    private string _startMusicID;

    [SerializeField]
    private float _volume;

    private EventInstance _eventInstance;


    private void Awake()
    {
        if (!Current)
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (VolumeManager.Current)
        {
            _volume = VolumeManager.Current.Music;
            VolumeManager.Current.SubscribeToChangeMusic(VolumeManager_OnChangeMusicVolume);
        }
        else
            Debug.LogError($"Failed to find VolumeManager");

        PlayMusic(_startMusicID);
    }

    public void SetVolume(float volume)
    {
        if (_eventInstance.isValid())
            _eventInstance.setVolume(volume);

        _volume = volume;
    }
    public void PlayMusic(string eventID)
    {
        if (_eventInstance.isValid())
        {
            _eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
        }

        _eventInstance = RuntimeManager.CreateInstance(eventID);
        _eventInstance.start();

        _eventInstance.setVolume(_volume);
    }

    private void VolumeManager_OnChangeMusicVolume(float volume) => SetVolume(volume);
}
