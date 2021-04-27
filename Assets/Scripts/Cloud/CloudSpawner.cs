using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    /// <summary>
    /// This script spawns clouds in rectangle area (leftUpPoint, rightDownPoint).
    /// </summary>
    
    [Header("Set in Inspector")]
    public GameObject cloudPrefab;

    public float _minSpawnerFrequency, _maxSpawnerFrequency;

    [SerializeField]
    private float _minDistanceFromSides;

    public List<GameObject> Clouds;

    public Camera _mainCamera;

    public delegate void SpawnDelegate();
    public event SpawnDelegate SpawnEvent;

    [SerializeField]
    private float _spawnerFrequency;
    [SerializeField]
    private float _timePassedSinceLastSpawn;

    [SerializeField]
    private bool _isSpawningEnabled;

    private void Start()
    {
        _mainCamera = Camera.main;
        Clouds = new List<GameObject>();
        _timePassedSinceLastSpawn = 0;
        UpdateSpawnerFrequency();
        PlayerProgressTracker.Current.SubscribeToOnProgressRemainingChanged(PlayerProgressTracker_OnProgressRemainingChanged);
    }


    void Update()
    {
        if (_isSpawningEnabled)
        {
            if (_timePassedSinceLastSpawn > _spawnerFrequency)
            {
                AddCloud();
                UpdateSpawnerFrequency();
                _timePassedSinceLastSpawn = 0;
                SpawnEvent?.Invoke();
            }
            else
            {
                _timePassedSinceLastSpawn += Time.deltaTime;
            }
        }
    }
    private void UpdateSpawnerFrequency() => _spawnerFrequency = UnityEngine.Random.Range(_minSpawnerFrequency, _maxSpawnerFrequency);
    private void AddCloud()
    {
        GameObject cloud = Instantiate(cloudPrefab);
        cloud.transform.SetParent(gameObject.transform);
        cloud.transform.position = GetRandomSpawnRangeOnVerticalAxis() - Vector2.up * 5;
        Clouds.Add(cloud);
    }

    private Vector2 GetRandomSpawnRangeOnVerticalAxis() => _mainCamera.ViewportToWorldPoint(new Vector2(UnityEngine.Random.Range(0 + _minDistanceFromSides, (float)1 - _minDistanceFromSides),0));

    private void PlayerProgressTracker_OnProgressRemainingChanged(float remainingProgress)
    {
        if (remainingProgress < 20)
            _isSpawningEnabled = false;
        else
            _isSpawningEnabled = true;
    }
}
