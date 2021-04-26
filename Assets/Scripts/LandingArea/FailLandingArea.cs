using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FailLandingArea : MonoBehaviour
{
    public static WinLandingArea Current;

    private event UnityAction OnLanded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerIdentifier.Current.gameObject)
        {
            OnLanded?.Invoke();
            Debug.LogError($"Failed");
        }
    }

    #region Event Subscription
    public void SubscribeToOnLanded(UnityAction callback) => HelperUtility.SubscribeTo(ref OnLanded, ref callback);
    public void UnsubscribeFromOnLanded(UnityAction callback) => HelperUtility.UnsubscribeFrom(ref OnLanded, ref callback);
    #endregion
}
