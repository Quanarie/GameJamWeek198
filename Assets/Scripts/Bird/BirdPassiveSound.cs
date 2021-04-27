using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPassiveSound : BirdSound
{
    [SerializeField]
    private string _deadSoundID;

    [SerializeField]
    private BirdHealth _birdHealth;

    private void Start()
    {
        if (!_birdHealth)
            _birdHealth = gameObject.GetComponent<BirdHealth>();

        if (_birdHealth)
            _birdHealth.SubscribeToOnDeath(BirdHealth_OnDeath);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find BirdHealth.");
    }

    private void BirdHealth_OnDeath(bool isDead)
    {
        if(isDead)
            PlaySound(_deadSoundID);
    }

    private void Update()
    {
        if(_isVisibleInCamera)
            PlayNormalSoundAtRegularIntervals();
    }
}
