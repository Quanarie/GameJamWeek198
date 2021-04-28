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

    private Dictionary<int, CatHealthUIChild> _healthDictionary = new Dictionary<int, CatHealthUIChild>();

    private int _numOfObjectsToCreate;
    private int _maxHealthPerLife;

    [SerializeField]
    private Transform _parent;
    private void Start()
    {
        if (PlayerIdentifier.Current)
        {
            CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();

            catHealth.SubscribeToOnDamageTaken(CatHealth_OnHealthChanged);
            catHealth.SubscribeToOnHealthGained(CatHealth_OnHealthChanged);
            catHealth.SubscribeToOnHealthReset(CatHealth_OnHealthReset);
            CreateHealthObjects(catHealth);
        }
    }

    private void CatHealth_OnHealthReset()
    {
        foreach (var item in _healthDictionary)
        {
            item.Value.UpdateImage(3);
        }
    }

    private void CreateHealthObjects(CatHealth catHealth)
    {
        _numOfObjectsToCreate = catHealth.GetMaxLife();
        _maxHealthPerLife = catHealth.GetMaxHealth();

        for (int i = 1; i < _numOfObjectsToCreate+1; i++)
        {
            GameObject objHealth = Instantiate(_objLife, _parent);
            CatHealthUIChild controller = objHealth.GetComponent<CatHealthUIChild>();
            if (controller)
            {
                controller.UpdateImage(_maxHealthPerLife);
                _healthDictionary.Add(i, controller);
            }
            else
                Debug.LogError($"{GetType().FullName} : Failed to find controller.");
        }
    }
    private void CatHealth_OnHealthChanged(int currentLife, int currentHealth)
    {
        if(_healthDictionary.ContainsKey(currentLife))
            _healthDictionary[currentLife].UpdateImage(currentHealth);
    }
}
