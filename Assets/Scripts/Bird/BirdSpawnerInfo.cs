using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to keep track of the Spawner that created this object.
/// When this object becomes inivisible, it will add itself to the object pool of the spawner.
/// </summary>
public class BirdSpawnerInfo : MonoBehaviour
{
    [SerializeField]
    private BirdSpawner _parentSpawner;

    public void Initialize(BirdSpawner birdSpawner)
    {
        _parentSpawner = birdSpawner;
    }

    private void OnBecameInvisible()
    {
        _parentSpawner.AddObjectToObjectPool(gameObject);
    }
}
