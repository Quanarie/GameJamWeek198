using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSoundAggressive : BirdSound
{
    [SerializeField]
    private bool _isNormalSoundActivated;

    [SerializeField]
    private string _attackSoundID;

    private void Start()
    {
        BirdAttackAI birdAttackAI = GetComponent<BirdAttackAI>();

        if (birdAttackAI)
        {
            birdAttackAI.SubscribeToOnAttackInitiated(BirdAttackAI_OnAttackInitiated);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find BirdAttackAI.");
    }

    private void Update()
    {
        if (_isVisibleInCamera)
            PlayNormalSoundAtRegularIntervals();
    }

    private void BirdAttackAI_OnAttackInitiated(bool isAttackInitiated)
    {
        if (isAttackInitiated)
        {
            _isNormalSoundActivated = false;
            PlaySound(_attackSoundID);
        }
        else
        {
            _isNormalSoundActivated = true;
        }
    }
}
