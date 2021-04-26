using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _expectedYAxisForCamera;

    [SerializeField]
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
        PlayerProgressTracker.Current.SubscribeToOnDestinationReached(PlayerProgressTracker_OnDestinationReached);
    }

    private void PlayerProgressTracker_OnDestinationReached()
    {
        
    }

    private IEnumerator PanCameraDown()
    {
        while()
    }
}