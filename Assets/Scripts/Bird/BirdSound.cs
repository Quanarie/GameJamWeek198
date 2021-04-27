using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BirdSound : SoundPlayer
{
    [SerializeField]
    protected bool _isVisibleInCamera;

    [SerializeField]
    protected string _normalBirdSoundID;

    [SerializeField]
    protected float _intervalBetweenNormalSounds, _timePassedSinceLastNormalSound;

    public void Initialize()
    {
        _isVisibleInCamera = true;
        _timePassedSinceLastNormalSound = 0;
    }
    protected void PlayNormalSoundAtRegularIntervals()
    {
        if (_timePassedSinceLastNormalSound >= _intervalBetweenNormalSounds)
        {
            _timePassedSinceLastNormalSound = 0;
            PlaySound(_normalBirdSoundID);
        }
        else
            _timePassedSinceLastNormalSound += Time.deltaTime;
    }

    private void OnBecameVisible()
    {
        _isVisibleInCamera = true;
    }

    private void OnBecameInvisible()
    {
        _isVisibleInCamera = false;
    }
}
