using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 _initialPosition;

    [SerializeField]
    private float _expectedYAxisForCamera;

    [SerializeField]
    private float _panningSpeed;

    [SerializeField]
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
        _initialPosition = _mainCamera.transform.position;
        PlayerProgressTracker.Current.SubscribeToOnDestinationReached(PlayerProgressTracker_OnDestinationReached);
    }

    public void ResetCameraPosition() => _mainCamera.transform.position = _initialPosition;
    private void PlayerProgressTracker_OnDestinationReached()
    {
        StartCoroutine(PanCameraDown());
    }

    private IEnumerator PanCameraDown()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while(_mainCamera.transform.position.y > _expectedYAxisForCamera)
        {
            Vector3 position = _mainCamera.transform.position;
            position.y -= Time.deltaTime * _panningSpeed;
            _mainCamera.transform.position = position;

            yield return waitForEndOfFrame;
        }
    }
}
