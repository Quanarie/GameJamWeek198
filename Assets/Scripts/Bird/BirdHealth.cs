using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class contains how health of birds should be handled.
/// </summary>
public class BirdHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _currentHealth;

    private event UnityAction<bool> OnDeath;
    public void Initialize(BirdHealthData birdHealthData)
    {
        _maxHealth = birdHealthData.MaxHealth;
        _currentHealth = _maxHealth;
        OnDeath?.Invoke(false);
    }

    /// <summary>
    /// Use this method to reduce the health
    /// </summary>
    /// <param name="healthToReduce"></param>
    public void ReduceHealth(float healthToReduce)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - healthToReduce, 0, _maxHealth);

        if(_currentHealth == 0)
        {
            //Probably a dirty way to get a kill counter. I will change this when I get time
            PlayerBirdKillTracker.Current.AddKillCount(1);
            if(PlayerBirdKillTracker.Current)
            OnDeath?.Invoke(true);
        }
    }

    #region Subscriptions
    public void SubscribeToOnDeath(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnDeath, ref callback);
    public void UnsubscribeFromOnDeath(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnDeath, ref callback);

    #endregion
}
