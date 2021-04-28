using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectUponGameEnded : MonoBehaviour
{
    [SerializeField]
    private CatHealth _catHealth;
    private void Start()
    {
        _catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
        _catHealth.SubscribeToOnDeath(CatHealth_OnDeath);

        LandingAreaManager.Current.SubscribeToOnLandingInWinArea(LandingAreaManager_OnLandingInWinArea);
    }

    private void LandingAreaManager_OnLandingInWinArea(bool isWin)
    {
        if(this)
            DestroyThisObject();
    }

    private void CatHealth_OnDeath(bool isDead)
    {
        if (this)
        {
            if (isDead)
            {
                _catHealth.UnsubscribeFromOnDeath(CatHealth_OnDeath);
                DestroyThisObject();
            }
        }
    }

    private void DestroyThisObject()
    {
        LandingAreaManager.Current.UnsubscribeFromOnLandingInWinArea(LandingAreaManager_OnLandingInWinArea);
        _catHealth.UnsubscribeFromOnDeath(CatHealth_OnDeath);
        if(gameObject)
            Destroy(gameObject);
    }
}
