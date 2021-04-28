using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSound : SoundPlayer
{
    [SerializeField]
    private string _cloudAttackSound;

    private void Start()
    {
        CloudAttack cloudAttack = GetComponent<CloudAttack>();
        if (cloudAttack)
        {
            cloudAttack.SubscribeToOnCloudAttack(CloudAttack_OnCloudAttack);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find Cloud Attack.");
    }
    private void CloudAttack_OnCloudAttack()
    {
        PlaySound(_cloudAttackSound);
    }
}
