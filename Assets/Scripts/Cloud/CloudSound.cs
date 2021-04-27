using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSound : SoundPlayer
{
    [SerializeField]
    private string _cloudSound;

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
        //Uncomment the line below when there is a sound for cloud
        //PlaySound(_cloudSound);
    }
}
