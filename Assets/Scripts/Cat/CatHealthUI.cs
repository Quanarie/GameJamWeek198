using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHealthUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objLives = new List<GameObject>();

    private void Start()
    {
        if (PlayerIdentifier.Current)
        {
            CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();

            catHealth.SubscribeToOnDamageTaken(CatHealth_OnDamageTaken);
        }
    }

    private void CatHealth_OnDamageTaken()
    {
        if(_objLives.Count > 0)
        {
            GameObject objLife = _objLives[Mathf.Min(0, _objLives.Count - 1)];
            objLife.SetActive(false);
            _objLives.Remove(objLife);
        }
        
    }
}
