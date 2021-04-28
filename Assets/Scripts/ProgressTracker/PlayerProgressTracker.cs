using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerProgressTracker : MonoBehaviour
{
    public static PlayerProgressTracker Current;

    [SerializeField]
    private float _currentProgress, _progressRequired;

    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private float _progressMultiplier;

    [SerializeField, Tooltip("Use this only for debugging")]
    private bool _isDestinationReached;

    [SerializeField]
    private Transform _player;

    private event UnityAction<float> OnProgressUpdated;
    private event UnityAction<float> OnProgressRemainingChanged;
    private event UnityAction OnDestinationReached;
    private void Awake()
    {
        Current = this;
    }

    private void Start()
    {
        if (PlayerIdentifier.Current)
            _player = PlayerIdentifier.Current.transform;
        else
            Debug.LogError($"{GetType().FullName} : Failed to find Player.");

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!_isDestinationReached)
        {
            _currentProgress += Time.deltaTime * GetSpeedScalerBasedOnPlayerYAxisLocation() * _progressMultiplier;
            _currentProgress = Mathf.Min(_currentProgress, _progressRequired);
            float remainingProgress = _progressRequired - _currentProgress;

            OnProgressUpdated?.Invoke(_currentProgress);
            OnProgressRemainingChanged?.Invoke(remainingProgress);

            if (_currentProgress == _progressRequired)
            {
                _isDestinationReached = true;
                OnDestinationReached?.Invoke();
            }
        }
    }

    public void ResetPlayerProgress()
    {
        _currentProgress = 0;
        _isDestinationReached = false;
        OnProgressRemainingChanged?.Invoke(_currentProgress);
    }
    public float GetMaxProgress() => _progressRequired;
    private float GetSpeedScalerBasedOnPlayerYAxisLocation()
    {
        if (_player)
        {
            Vector2 bottomLeftCorner = _mainCamera.ViewportToWorldPoint(new Vector2(0, 1));
            Vector2 topRightCorner = _mainCamera.ViewportToWorldPoint(new Vector2(1, 0));

            float maxY = bottomLeftCorner.y;
            float minY = topRightCorner.y;

            float remappedValue = _player.transform.position.y.MapValue(minY, maxY, 0, 1);
            return 1 - remappedValue;
        }
        else
        {
            Debug.LogError($"{GetType().FullName} : Player is missing.");
            return 0;
        }
    }

    #region Event Subscription
    public void SubscribeToOnProgressUpdated(UnityAction<float> callback) => HelperUtility.SubscribeTo(ref OnProgressUpdated, ref callback);
    public void UnsubscribeFromOnProgressUpdated(UnityAction<float> callback) => HelperUtility.UnsubscribeFrom(ref OnProgressUpdated, ref callback);

    public void SubscribeToOnProgressRemainingChanged(UnityAction<float> callback) => HelperUtility.SubscribeTo(ref OnProgressRemainingChanged, ref callback);
    public void UnsubscribeFromOnProgressRemainingChanged(UnityAction<float> callback) => HelperUtility.UnsubscribeFrom(ref OnProgressRemainingChanged, ref callback);

    public void SubscribeToOnDestinationReached(UnityAction callback) => HelperUtility.SubscribeTo(ref OnDestinationReached, ref callback);
    public void UnsubscribeFromOnDestinationReached(UnityAction callback) => HelperUtility.UnsubscribeFrom(ref OnDestinationReached, ref callback);
    #endregion
}
