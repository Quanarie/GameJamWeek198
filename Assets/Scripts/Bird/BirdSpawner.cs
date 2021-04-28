using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will create bird ojects either at left or right side of the screen periodically.
/// If there are objects in objectPool, then uses one of them instead.
/// </summary>
public class BirdSpawner : MonoBehaviour
{
    /// <summary>
    /// Singleton Accessor
    /// </summary>
    public static BirdSpawner Current;

    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private float _minSpawnerFrequency, _maxSpawnerFrequency, _spawnerFrequency, _minSpawnDistanceFromSides;

    /// <summary>
    /// Keeps track of time since last spawn
    /// </summary>
    [SerializeField]
    private float _timePassedSinceLastSpawn;

    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    [SerializeField]
    private GameObject _prefabBird;

    [SerializeField]
    private bool _isSpawningEnabled;


    private void Awake()
    {
        Current = this;
    }
    private void Start()
    {
        if(!_mainCamera)
            _mainCamera = Camera.main;

        _spawnerFrequency = _maxSpawnerFrequency;

        PlayerProgressTracker.Current.SubscribeToOnProgressRemainingChanged(PlayerProgressTracker_OnProgressRemainingChanged);
    }

    private void Update()
    {
        if (_isSpawningEnabled)
        {
            AttemptToSpawnObject();
        }
    }

    #region Public Methods

    public void ResetBirdSpawner()
    {
        _objectPool.Clear();
    }

    /// <summary>
    /// This function can be used to add a game object to the object pool.
    /// </summary>
    /// <param name="objectToAdd"></param>
    public void AddObjectToObjectPool(GameObject objectToAdd)
    {
        if (!_objectPool.Contains(objectToAdd))
            _objectPool.Enqueue(objectToAdd);
    }

    /// <summary>
    /// This function is used to activate spawning.
    /// </summary>
    /// <param name="isEnabled"></param>
    public void EnableSpawning(bool isEnabled) => _isSpawningEnabled = isEnabled;
    #endregion

    #region Private Methods
    /// <summary>
    /// This function attempts to spawn object if sufficient time has passed
    /// </summary>
    private void AttemptToSpawnObject()
    {
        if (_timePassedSinceLastSpawn >= _spawnerFrequency)
        {
            _timePassedSinceLastSpawn -= _spawnerFrequency;

            if (_objectPool.Count > 0)
                UseObjBirdFromObjectPool();
            else
                CreateObjectFromPrefab();

            UpdateSpawnerFrequency();
        }
        else
            _timePassedSinceLastSpawn += Time.deltaTime;
    }

    /// <summary>
    /// This function updates spawner frequency until max frequency is met
    /// </summary>
    private void UpdateSpawnerFrequency() => _spawnerFrequency = UnityEngine.Random.Range(_minSpawnerFrequency, _maxSpawnerFrequency);
    /// <summary>
    /// This function takes an object from object pool and moves it to a new origin location
    /// </summary>
    private void UseObjBirdFromObjectPool()
    {
        if(_objectPool.Count > 0)
        {
            bool isSpawningAtRightSide = UnityEngine.Random.Range(0, 100) > 50;
            Vector2 spawnPosition = GenerateStartPosition(isSpawningAtRightSide ? Direction.Right : Direction.Left);

            GameObject objBirdFromObjectPool = _objectPool.Dequeue();
            BirdStartData birdStartData = new BirdStartData(spawnPosition,
                                                            Quaternion.Euler(0, isSpawningAtRightSide ? 0 : 180, 0),
                                                            isSpawningAtRightSide ? Direction.Left : Direction.Right);
            objBirdFromObjectPool.SetActive(true);

            BirdInitializer birdInitializer = objBirdFromObjectPool.GetComponent<BirdInitializer>();
            if (birdInitializer)
                birdInitializer.Initialize(birdStartData);
            else
                Debug.LogError($"{GetType().FullName} : Failed to find BirdInitializer on BirdObject.");
        }
    }

    /// <summary>
    /// This method creates an object using the prefab at a location and initializes it.
    /// </summary>
    private void CreateObjectFromPrefab()
    {
        if (_prefabBird)
        {
            bool isSpawningAtRightSide = UnityEngine.Random.Range(0, 100) > 50;
            Vector2 spawnPosition = GenerateStartPosition(isSpawningAtRightSide ? Direction.Right : Direction.Left);

            BirdStartData birdStartData = new BirdStartData(spawnPosition,
                                                            Quaternion.Euler(0, isSpawningAtRightSide ? 0 : 180, 0),
                                                            isSpawningAtRightSide ? Direction.Left : Direction.Right);

            GameObject objBird = Instantiate(_prefabBird);

            BirdInitializer birdInitializer = objBird.GetComponent<BirdInitializer>();
            if (birdInitializer) 
            {
                birdInitializer.Initialize(birdStartData);
                BirdSpawnerInfo birdSpawnerInfo = objBird.AddComponent<BirdSpawnerInfo>();
                birdSpawnerInfo.Initialize(this);
            } 
            else
                Debug.LogError($"{GetType().FullName} : Failed to find BirdInitializer on BirdObject.");
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find Prefab to create Game Object.");
    }

    /// <summary>
    /// Generates a random position depending on the direction provided.
    /// </summary>
    /// <param name="spawnDirection"></param>
    /// <returns></returns>
    private Vector2 GenerateStartPosition(Direction spawnDirection)
    {
        bool isRight = spawnDirection == Direction.Right;
        if(_mainCamera)
            return _mainCamera.ViewportToWorldPoint(new Vector2(isRight ? 1 - _minSpawnDistanceFromSides : 0 + _minSpawnDistanceFromSides, UnityEngine.Random.Range(0, (float)0.6f)));
        else
        {
            Debug.LogError($"{GetType().FullName} : Failed to find MainCamera.");
            return Vector2.zero;
        }
    }

    private void PlayerProgressTracker_OnProgressRemainingChanged(float remainingProgress)
    {
        if (remainingProgress < 20)
            _isSpawningEnabled = false;
        else
            _isSpawningEnabled = true;
    }

    #endregion
}
