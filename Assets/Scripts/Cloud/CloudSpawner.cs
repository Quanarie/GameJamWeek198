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
    public Vector2 leftUpPoint;    // this coordinates limit the area where clouds will spawn (relatively to the connected object) (this area should be under the camera)
    public Vector2 rightDownPoint;
    public float _minSpawnerFrequency, _maxSpawnerFrequency;
    public List<GameObject> Clouds;

    public delegate void SpawnDelegate();
    public event SpawnDelegate SpawnEvent;

    private float _spawnerFrequency;
    private float _timePassedSinceLastSpawn;


    private void Start()
    {
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
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(transform.position.x + leftUpPoint.x, transform.position.x + rightDownPoint.x);
        pos.y = Random.Range(transform.position.y + rightDownPoint.y, transform.position.y + leftUpPoint.y);
        cloud.transform.position = pos;
        Clouds.Add(cloud);
    }
}
