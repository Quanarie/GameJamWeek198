using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloudAttack : MonoBehaviour
{
    [SerializeField]
    private int _attackDamage;

    private UnityAction OnCloudAttack;


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (PlayerIdentifier.Current.gameObject == collision.gameObject)
    //    {
    //        CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
    //        catHealth.ReduceHealth(_attackDamage);
    //        Debug.LogError($"Collider");
    //    }
    //}

    /// <summary>
    /// This is a temporary fix for cloud attack. This function should be removed at a later stage to respect cat's attack range trigger and cat's collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerIdentifier.Current.gameObject == collision.gameObject)
        {
            OnCloudAttack?.Invoke();
            CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
            catHealth.ReduceHealth(_attackDamage);
        }
    }

    #region Event Subscription
    public void SubscribeToOnCloudAttack(UnityAction callback) => HelperUtility.SubscribeTo(ref OnCloudAttack, ref callback);
    public void UnsubscribeFromOnCloudAttack(UnityAction callback) => HelperUtility.UnsubscribeFrom(ref OnCloudAttack, ref callback);
    #endregion
}
