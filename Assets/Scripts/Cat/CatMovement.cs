using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField, Tooltip("Donot change these values in inspector.")]
    private float _maxY, _maxX, _minY, _minX;

    [SerializeField]
    private float _maxRiseSpeed, _maxFallSpeed, _maxSlideSpeed;
    [SerializeField]
    private float _minDistanceFromBottom, _minDistanceFromTop, _minDistanceFromSide;

    [SerializeField]
    private Parachute _parachute;
    [SerializeField]
    private ParachuteMode _parachuteMode;
    private Camera _mainCamera;

    private Rigidbody2D _catRB;

    void Start()
    {
        _mainCamera = Camera.main;
        if (!_parachute)
        {
            _parachute = GetComponent<Parachute>();
            if (!_parachute)
            {
                _parachute = GetComponentInChildren<Parachute>();
                _parachute.SubscribeToOnParachuteModeChanged(Parachute_OnParachuteModeChanged);
            }
                
            else
                Debug.LogError($"{GetType().FullName} : Failed to find Parachute.");
        }

        FindMinMaxValuesOfAxisWithinCameraView();
        _catRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FindMinMaxValuesOfAxisWithinCameraView();
        if (_parachuteMode == ParachuteMode.Open)
            Rise();
        else
            FreeFall();

        HorizontalMove();
    }

    private void HorizontalMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 position = gameObject.transform.position - new Vector3(gameObject.transform.right.normalized.x * _maxSlideSpeed, 0, 0) * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, _minX + _minDistanceFromSide, _maxX);
            gameObject.transform.position = position;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Vector3 position = gameObject.transform.position + new Vector3(gameObject.transform.right.normalized.x * _maxSlideSpeed, 0, 0) * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, _minX , _maxX - _minDistanceFromSide);
            gameObject.transform.position = position;
        }

    }
    private void FindMinMaxValuesOfAxisWithinCameraView()
    {
        Vector2 topRightCorner = _mainCamera.ViewportToWorldPoint(new Vector2(1, 0));
        _minY = topRightCorner.y;
        _maxX = topRightCorner.x;

        Vector2 bottomLeftCorner = _mainCamera.ViewportToWorldPoint(new Vector2(0, 1));
        _maxY = bottomLeftCorner.y;
        _minX = bottomLeftCorner.x;
    }

    private float GetFreeFallSpeed()
    {
        float fallspeed = gameObject.transform.position.y.MapValue(_minY, _maxY, 0, 1);
        return (fallspeed - _minDistanceFromBottom) * _maxFallSpeed;
    }
    private float GetRiseSpeed()
    {
        float fallspeed = gameObject.transform.position.y.MapValue(_minY, _maxY, 0, 1);
        fallspeed += _minDistanceFromTop;
        return (1 - fallspeed) * _maxRiseSpeed;
    }

    private void Rise()
    {
        Vector3 speed = new Vector3(0, Time.deltaTime * GetRiseSpeed(), 0);
        gameObject.transform.position += speed;
    }
    private void FreeFall()
    {
        Vector3 speed = new Vector3(0, Time.deltaTime * GetFreeFallSpeed(), 0);
        gameObject.transform.position -= speed;
    }

    private void Parachute_OnParachuteModeChanged(ParachuteMode parachuteMode) => _parachuteMode = parachuteMode;
}
