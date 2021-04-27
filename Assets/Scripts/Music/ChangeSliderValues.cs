using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSliderValues : MonoBehaviour
{
    [SerializeField]
    private Slider SFXSlider;
    [SerializeField]
    private Slider musicSlider;

    private void Start()
    {
        VolumeManager volumeManager = GetComponent<VolumeManager>();
        if (volumeManager)
        {
            SFXUpdate(volumeManager.SFX);
            MusicUpdate(volumeManager.Music);
        }
        else
        {
            Debug.LogError($"{GetType().FullName} : Failed to find VolumeManager.");
        }
    }
    private void SFXUpdate(float volume)
    {
        if (SFXSlider)
            SFXSlider.value = volume;
        else
            Debug.LogError($"{GetType().FullName} : Failed to find slider for SFX volume.");
    }
    private void MusicUpdate(float volume)
    {
        if (musicSlider)
            musicSlider.value = volume;
        else
            Debug.LogError($"{GetType().FullName} : Failed to find slider for SFX volume.");
    }
}