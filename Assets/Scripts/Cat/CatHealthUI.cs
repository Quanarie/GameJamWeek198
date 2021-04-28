using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHealthUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objLives = new List<GameObject>();

    [SerializeField]
    private GameObject _objLife;

    [SerializeField]
    private Transform _parent;
    private void Start()
    {
        if (PlayerIdentifier.Current)
        {
            CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();

            catHealth.SubscribeToOnDamageTaken(CatHealth_OnHealthChanged);
            catHealth.SubscribeToOnHealthGained(CatHealth_OnHealthChanged);
            CreateLifeObjects(catHealth.GetCurrentHealth());
        }
    }

    private void CreateLifeObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject objLife = Instantiate(_objLife, _parent);
            objLife.SetActive(true);
            _objLives.Add(objLife);
        }
    }
    private void CatHealth_OnHealthChanged(int currentHealth)
    {
        if(_objLives.Count > 0)
        {
            for (int i = 0; i < _objLives.Count; i++)
            {
                if (i <= currentHealth - 1)
                {
                    _objLives[i].SetActive(true);
                }
                else
                    _objLives[i].SetActive(false);
            }
        }
        
    }
}
