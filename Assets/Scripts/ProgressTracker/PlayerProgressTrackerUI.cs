using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressTrackerUI : MonoBehaviour
{
    [SerializeField]
    private Slider _sldProgress;

    [SerializeField]
    private TextMeshProUGUI _txtProgress;

    [SerializeField]
    private float _maxProgressRequired;

    
    private void Start()
    {
        if (PlayerProgressTracker.Current)
        {
            PlayerProgressTracker.Current.SubscribeToOnProgressUpdated(PlayerProgressTracker_OnProgressUpdated);
            if (_sldProgress)
            {
                _sldProgress.maxValue = PlayerProgressTracker.Current.GetMaxProgress();
                _maxProgressRequired = PlayerProgressTracker.Current.GetMaxProgress();
            }
                
        }   
    }

    private void PlayerProgressTracker_OnProgressUpdated(float progress)
    {
        if (_sldProgress)
            _sldProgress.value = progress;
        else
            Debug.LogError($"{GetType().FullName} : Failed to find slider for progress.");

        if (_txtProgress)
            _txtProgress.text = $"{(int)(_maxProgressRequired - progress)}";
    }
}
