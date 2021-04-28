using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSound : SoundPlayer
{
    [SerializeField]
    private string _catAttackID;

    [SerializeField]
    private string _catGetHitID;

    private void Start()
    {
        CatAttack catAttack = gameObject.GetComponent<CatAttack>();
        if (catAttack)
        {
            catAttack.SubscribeToOnAttackInitiated(CatAttack_OnAttackInitiated);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find CatAttack.");

        CatHealth catHealth = gameObject.GetComponent<CatHealth>();
        if (catHealth)
        {
            catHealth.SubscribeToOnDamageTaken(CatHealth_OnDamageTaken);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find CatAttack.");
    }

    private void CatHealth_OnDamageTaken(int currentHealth)
    {
        PlaySound(_catGetHitID);
    }

    private void CatAttack_OnAttackInitiated(bool isAttackInitiated)
    {
        if (isAttackInitiated)
        {
            //PlaySound(_catAttackID);
        }
    }
}
