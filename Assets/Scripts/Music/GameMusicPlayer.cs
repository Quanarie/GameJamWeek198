using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.LogError($"{GetType().FullName} : Remove this part of the code after testing.");
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
}
