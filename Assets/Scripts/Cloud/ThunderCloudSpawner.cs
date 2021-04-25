using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloudSpawner : MonoBehaviour
{
    /// <summary>
    /// This script spawns clouds while cat is falling and deleting them when it is out of camera vision
    /// It should be connected to the Cat object
    /// </summary>
    
    [Header("Set in Inspector")]
    public GameObject cloudPrefab;
    public Vector2 leftUpPoint;    // this coordinates limit the area where clouds will spawn (relatively to the cat) (this area should be under the camera)
    public Vector2 rightDownPoint;

    public float _minSpawnerFrequency, _maxSpawnerFrequency;
    private float _spawnerFrequency;
    private float _timePassedSinceLastSpawn;

    private List<GameObject> Clouds;

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
            GameObject cloud = Instantiate(cloudPrefab);
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(transform.position.x + leftUpPoint.x, transform.position.x + rightDownPoint.x);
            pos.y = Random.Range(transform.position.y + rightDownPoint.y, transform.position.y + leftUpPoint.y);
            cloud.transform.position = pos;
            Clouds.Add(cloud);
            _timePassedSinceLastSpawn = 0;
            UpdateSpawnerFrequency();
        }
        else
        {
            _timePassedSinceLastSpawn += Time.deltaTime;
        }

        for (int i = 0; i < Clouds.Count; i++)
        {
            if (Clouds[i].transform.position.y - Camera.main.transform.position.y > Camera.main.orthographicSize)
            {
                Destroy(Clouds[i]);
                Clouds.Remove(Clouds[i]);
            }
        }
    }
    private void UpdateSpawnerFrequency() => _spawnerFrequency = UnityEngine.Random.Range(_minSpawnerFrequency, _maxSpawnerFrequency);
}
