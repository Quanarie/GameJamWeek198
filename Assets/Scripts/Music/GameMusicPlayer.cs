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

    private EventInstance _eventInstance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayMusic(_gameplayMusicID);
        }else if (Input.GetKey(KeyCode.A))
        {
            PlayMusic(_introMusicID);
        }
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
        _eventInstance.setVolume(100);
    }
}
