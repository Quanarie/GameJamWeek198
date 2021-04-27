
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandingAreaManager : MonoBehaviour
{
    public static LandingAreaManager Current;

    [SerializeField]
    private WinLandingArea _winLandArea;

    [SerializeField]
    private List<FailLandingArea> _failLandArea = new List<FailLandingArea>();

    private event UnityAction<bool> OnLandingInWinArea;

    private void Awake()
    {
        Current = this;

        if (_winLandArea)
        {
            _winLandArea.SubscribeToOnLanded(WinLandArea_OnLanded);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find WinLandArea");

        if(_failLandArea.Count > 0)
        {
            _failLandArea.ForEach(c => c.SubscribeToOnLanded(FailLandArea_OnLanded));
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find FailLandArea");

    }

    private void FailLandArea_OnLanded() =>  OnLandingInWinArea?.Invoke(false);

    private void WinLandArea_OnLanded() => OnLandingInWinArea?.Invoke(true);

    #region Event Subscription
    public void SubscribeToOnLandingInWinArea(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnLandingInWinArea, ref callback);
    public void UnsubscribeFromOnLandingInWinArea(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnLandingInWinArea, ref callback);
    #endregion
}
