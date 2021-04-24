using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for AI for flying birds
/// </summary>
public abstract class BirdFlyAI : MonoBehaviour, IBirdFlyAIInitializer
{
    [SerializeField]
    protected Direction _flyDirection;

    [SerializeField]
    protected FlyMode _flyMode;

    [SerializeField]
    protected float _normalFlySpeed;

    public event UnityAction<FlyMode> OnFlyModeChanged;

    /// <summary>
    /// Use this function to initialize Bird Fly AI
    /// </summary>
    /// <param name="flyDirection"></param>
    /// <param name="birdFlyData"></param>
    public abstract void Initialize(Direction flyDirection, BirdFlyData birdFlyData);

    public FlyMode GetCurrentFlyMode() => _flyMode;

    /// <summary>
    /// This function takes in flyMode and flies the bird defined by the mode.
    /// </summary>
    /// <param name="flyMode"></param>
    protected abstract void Fly(FlyMode flyMode);

    protected void ChangeFlyMode(FlyMode flyMode)
    {
        _flyMode = flyMode;
        OnFlyModeChanged?.Invoke(_flyMode);
    }

    #region Event Subscription
    public void SubscribeToOnFlyModeChanged(UnityAction<FlyMode> callback) => HelperUtility.SubscribeTo(ref OnFlyModeChanged, ref callback);
    public void UnsubscribeFromOnFlyModeChanged(UnityAction<FlyMode> callback) => HelperUtility.UnsubscribeFrom(ref OnFlyModeChanged, ref callback);
    #endregion
}
