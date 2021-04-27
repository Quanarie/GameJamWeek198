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
    private event UnityAction OnDamageTaken;


    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Use this method to reduce the health
    /// </summary>
    /// <param name="healthToReduce"></param>
    public void ReduceHealth(float healthToReduce)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - healthToReduce, 0, _maxHealth);

        OnDamageTaken?.Invoke();

        if (_currentHealth == 0)
        {
            OnDeath?.Invoke(true);
        }
    }

    #region Subscriptions
    public void SubscribeToOnDeath(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnDeath, ref callback);
    public void UnsubscribeFromOnDeath(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnDeath, ref callback);
    public void SubscribeToOnDamageTaken(UnityAction callback) => HelperUtility.SubscribeTo(ref OnDamageTaken, ref callback);
    public void UnsubscribeFromOnDamageTakenh(UnityAction callback) => HelperUtility.UnsubscribeFrom(ref OnDamageTaken, ref callback);

    #endregion
}
