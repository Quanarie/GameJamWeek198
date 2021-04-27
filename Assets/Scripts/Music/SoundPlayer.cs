using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private float _volume;

    private EventInstance _eventInstance;

    private void Start()
    {
        Debug.LogError($"Connect to volume manager and get volume");
    }
    protected void PlaySound(string eventID)
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

    private void StopSound()
    {
        if (_eventInstance.isValid())
        {
            _eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
        }
    }
    private void UpdateVolume(float newVolume)
    {
        _volume = newVolume;
        if (_eventInstance.isValid())
        {
            _eventInstance.setVolume(_volume);
        }
    }

    private void OnBecameInvisible()
    {
        StopSound();
    }
    private void OnDestroy()
    {
        Debug.LogError($"Unsubscribe From VolumeManager.");
    }
}
