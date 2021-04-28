using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneAnimationSound : MonoBehaviour
{
    [SerializeField]
    private float _sfxVolume;

    [SerializeField]
    private GameObject _objMainMenu, _objTitle;

    private List<EventInstance> _eventInstances = new List<EventInstance>();

    private void Start()
    {
        _sfxVolume = VolumeManager.Current.SFX;
        VolumeManager.Current.SubscribeToChangeSFX(VolumeManager_OnSFXChanged);
    }

    private void VolumeManager_OnSFXChanged(float sfxVolume)
    {
        _sfxVolume = sfxVolume;
    }

    public void ShowTitle()
    {
        _objTitle.SetActive(true);
    }
    public void DisableMainMenu()
    {
        _objMainMenu.SetActive(false);
    }
    public void PlayCatGotKickedSound()
    {
        PlaySound("event:/CatKickedSound");
    }
    public void PlayKickSound()
    {
        PlaySound("event:/KickSound");
    }
    public void PlayPlaneSound()
    {
        PlaySound("event:/PlaneSound");
    } 

    public void PlayOpenDoorSound()
    {
        PlaySound("event:/OpenDoor");
    }

    private void PlaySound(string eventID)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventID);
        eventInstance.start();
        eventInstance.setVolume(_sfxVolume);
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _eventInstances.Count; i++)
        {
            if (_eventInstances[i].isValid())
            {
                _eventInstances[i].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _eventInstances[i].release();
            }
        }
    }

}
