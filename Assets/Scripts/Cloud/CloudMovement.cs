using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    /// <summary>
    /// This script moves all clouds from CloudSpawner.Clouds upper
    /// </summary>
    
    [Header("Set in Inspector")]
    public float cloudSpeed;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Camera _mainCamera;

    private void Start()
    {
        _player = PlayerIdentifier.Current.transform;
        if (!_player)
            Debug.LogError($"{GetType().FullName} : Failed to find Player.");

        _mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += Time.deltaTime * cloudSpeed * GetSpeedScalerBasedOnPlayerYAxisLocation();

        transform.position = pos;
    }

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
}
