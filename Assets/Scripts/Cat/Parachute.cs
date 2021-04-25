using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Parachute : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _parachuteRenderer;
    private ParachuteMode _currentMode;

    private event UnityAction<ParachuteMode> OnParachuteModeChanged;

    private void Awake()
    {
        if(!_parachuteRenderer)
        {
            _parachuteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentMode == ParachuteMode.Close)
                _currentMode = ParachuteMode.Open;
            else
                _currentMode = ParachuteMode.Close;

            UpdateParachuteRenderer(_currentMode);
            OnParachuteModeChanged?.Invoke(_currentMode);
        }
    }

    private void UpdateParachuteRenderer(ParachuteMode parachuteMode)
    {
        if (_parachuteRenderer)
        {
            if (parachuteMode == ParachuteMode.Close)
                _parachuteRenderer.enabled = false;
            else
                _parachuteRenderer.enabled = true;
        }
        else
            Debug.LogError($"{GetType().FullName} : Parachute Renderer is missing.");
        
    }

    #region Event Subscription
    public void SubscribeToOnParachuteModeChanged(UnityAction<ParachuteMode> callback) => HelperUtility.SubscribeTo(ref OnParachuteModeChanged, ref callback);
    public void UnsubscribeFromOnParachuteModeChanged(UnityAction<ParachuteMode> callback) => HelperUtility.UnsubscribeFrom(ref OnParachuteModeChanged, ref callback);
    #endregion
}