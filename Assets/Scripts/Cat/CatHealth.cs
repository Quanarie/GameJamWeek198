using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _currentHealth;

    private event UnityAction<bool> OnDeath;

    /// <summary>
    /// Use this method to reduce the health
    /// </summary>
    /// <param name="healthToReduce"></param>
    public void ReduceHealth(float healthToReduce)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - healthToReduce, 0, _maxHealth);

        if (_currentHealth == 0)
        {
            OnDeath?.Invoke(true);
        }
    }

    #region Subscriptions
    public void SubscribeToOnDeath(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnDeath, ref callback);
    public void UnsubscribeFromOnDeath(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnDeath, ref callback);

    #endregion
}
