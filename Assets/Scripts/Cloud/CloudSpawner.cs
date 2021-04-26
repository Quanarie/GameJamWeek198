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

    private float _spawnerFrequency;
    private float _timePassedSinceLastSpawn;


    private void Start()
    {
        _mainCamera = Camera.main;
        Clouds = new List<GameObject>();
        _timePassedSinceLastSpawn = 0;
        UpdateSpawnerFrequency();
    }
    void Update()
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
    private void UpdateSpawnerFrequency() => _spawnerFrequency = UnityEngine.Random.Range(_minSpawnerFrequency, _maxSpawnerFrequency);
    private void AddCloud()
    {
        GameObject cloud = Instantiate(cloudPrefab);
        cloud.transform.SetParent(gameObject.transform);
        cloud.transform.position = GetRandomSpawnRangeOnVerticalAxis();
        Clouds.Add(cloud);
    }

    private Vector2 GetRandomSpawnRangeOnVerticalAxis() => _mainCamera.ViewportToWorldPoint(new Vector2(UnityEngine.Random.Range(0 + _minDistanceFromSides, (float)1 - _minDistanceFromSides),0));

}
