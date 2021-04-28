using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;

    [SerializeField]
    private int _currentHealth;

    private event UnityAction<bool> OnDeath;
    private event UnityAction<int> OnDamageTaken;
    private event UnityAction<int> OnHealthGained;


    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public int GetMaxHealth() => _maxHealth;
    public int GetCurrentHealth() => _currentHealth;
    public void AddHealth(int healthToAdd)
    {
        _currentHealth = Mathf.Min(_currentHealth + healthToAdd, _maxHealth);
        OnHealthGained?.Invoke(_currentHealth);
    }

    /// <summary>
    /// Use this method to reduce the health
    /// </summary>
    /// <param name="healthToReduce"></param>
    public void ReduceHealth(int healthToReduce)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - healthToReduce, 0, _maxHealth);

        OnDamageTaken?.Invoke(_currentHealth);

        if (_currentHealth == 0)
        {
            OnDeath?.Invoke(true);
        }
    }

    #region Subscriptions
    public void SubscribeToOnDeath(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnDeath, ref callback);
    public void UnsubscribeFromOnDeath(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnDeath, ref callback);
    public void SubscribeToOnDamageTaken(UnityAction<int> callback) => HelperUtility.SubscribeTo(ref OnDamageTaken, ref callback);
    public void UnsubscribeFromOnDamageTaken(UnityAction<int> callback) => HelperUtility.UnsubscribeFrom(ref OnDamageTaken, ref callback);

    public void SubscribeToOnHealthGained(UnityAction<int> callback) => HelperUtility.SubscribeTo(ref OnDamageTaken, ref callback);
    public void UnsubscribeFromOnHealthGained(UnityAction<int> callback) => HelperUtility.UnsubscribeFrom(ref OnDamageTaken, ref callback);

    #endregion
}
