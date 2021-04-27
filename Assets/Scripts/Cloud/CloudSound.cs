using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSound : SoundPlayer
{
    [SerializeField]
    private string _cloudIdleSound;

    [SerializeField]
    private string _cloudAttackSound;

    [SerializeField]
    private float _volumeIdle;

    private EventInstance _eventInstance;

    private void Start()
    {
        Debug.LogError($"Connect to volume manager and get volume");

        CloudAttack cloudAttack = GetComponent<CloudAttack>();
        if (cloudAttack)
        {
            cloudAttack.SubscribeToOnCloudAttack(CloudAttack_OnCloudAttack);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find Cloud Attack.");

        if (VolumeManager.Current)
        {
            _volumeIdle = VolumeManager.Current.SFX;
            VolumeManager.Current.SubscribeToChangeSFX(VolumeManager_OnChangeSFX);
        }
        else
            Debug.LogError($"Failed to find VolumeManager");
    }

    private void PlayCloudIdleSound()
    {
        if (_eventInstance.isValid())
        {
            _eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
        }

        _eventInstance = RuntimeManager.CreateInstance(_cloudIdleSound);
        _eventInstance.start();
        _eventInstance.setVolume(_volumeIdle);
    }

    private void StopCloudIdleSound()
    {
        if (_eventInstance.isValid())
        {
            _eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
        }
    }
    private void CloudAttack_OnCloudAttack()
    {
        PlaySound(_cloudAttackSound);
    }
    private void UpdateVolume(float newVolume)
    {
        _volumeIdle = newVolume;
        if (_eventInstance.isValid())
        {
            _eventInstance.setVolume(_volumeIdle);
        }
    }

    private void OnBecameVisible()
    {
        PlayCloudIdleSound();
    }

    protected void OnBecameInvisible()
    {
        StopCloudIdleSound();
    }
    private void OnDestroy()
    {
        if (VolumeManager.Current)
        {
            VolumeManager.Current.UnsubscribeFromChangeSFX(VolumeManager_OnChangeSFX);
        }
        else
            Debug.LogError($"Failed to find VolumeManager");
    }

    private void VolumeManager_OnChangeSFX(float volume) => UpdateVolume(volume);
}
