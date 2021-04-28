using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxNumOfLives;
    [SerializeField]
    private int _currentNumOfLives;
    [SerializeField]
    private int _maxHealthPerLife;

    [SerializeField]
    private int _currentHealth;

    private event UnityAction<bool> OnDeath;

    /// <summary>
    /// First value is number of remaining life. Second value is remaining health for the current life.
    /// </summary>
    private event UnityAction<int, int> OnDamageTaken;
    /// <summary>
    /// First value is number of remaining life. Second value is remaining health for the current life.
    /// </summary>
    private event UnityAction<int, int> OnHealthGained;

    private event UnityAction OnHealthReset;


    private void Awake()
    {
        _currentHealth = _maxHealthPerLife;
        _currentNumOfLives = _maxNumOfLives;
    }

    public void ResetCatHealth()
    {
        _currentNumOfLives = _maxNumOfLives;
        _currentHealth = _maxHealthPerLife;
        OnHealthReset?.Invoke();
    }
    public int GetMaxHealth() => _maxHealthPerLife;
    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxLife() => _maxNumOfLives;
    public int GetCurrentLife() => _currentNumOfLives;
    public void AddHealth(int healthToAdd)
    {
        _currentHealth = Mathf.Min(_currentHealth + healthToAdd, _maxHealthPerLife);
        OnHealthGained?.Invoke(_currentNumOfLives, _currentHealth);
    }

    /// <summary>
    /// Use this method to reduce the health
    /// </summary>
    /// <param name="healthToReduce"></param>
    public void ReduceHealth(int healthToReduce)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - healthToReduce, 0, _maxHealthPerLife);
        OnDamageTaken?.Invoke(_currentNumOfLives, _currentHealth);
        if(_currentHealth == 0)
        {
            _currentHealth = _maxHealthPerLife;
            _currentNumOfLives = Mathf.Max(_currentNumOfLives - 1, 0);
        }

        OnDamageTaken?.Invoke(_currentNumOfLives, _currentHealth);

        if (_currentNumOfLives == 0)
        {
            Debug.LogError("CAT IS DEAD");
            OnDeath?.Invoke(true);
        }
    }

    #region Subscriptions
    public void SubscribeToOnDeath(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnDeath, ref callback);
    public void UnsubscribeFromOnDeath(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnDeath, ref callback);
    public void SubscribeToOnDamageTaken(UnityAction<int,int> callback) => HelperUtility.SubscribeTo(ref OnDamageTaken, ref callback);
    public void UnsubscribeFromOnDamageTaken(UnityAction<int,int> callback) => HelperUtility.UnsubscribeFrom(ref OnDamageTaken, ref callback);

    public void SubscribeToOnHealthGained(UnityAction<int,int> callback) => HelperUtility.SubscribeTo(ref OnHealthGained, ref callback);
    public void UnsubscribeFromOnHealthGained(UnityAction<int,int> callback) => HelperUtility.UnsubscribeFrom(ref OnHealthGained, ref callback);
    public void SubscribeToOnHealthReset(UnityAction callback) => HelperUtility.SubscribeTo(ref OnHealthReset, ref callback);
    public void UnsubscribeFromOnHealthReset(UnityAction callback) => HelperUtility.UnsubscribeFrom(ref OnHealthReset, ref callback);


    #endregion
}
