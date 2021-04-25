using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to keep track of the Spawner that created this object.
/// When this object becomes invisible or dead, it will add itself to the object pool of the spawner.
/// </summary>
public class BirdSpawnerInfo : MonoBehaviour
{
    [SerializeField]
    private BirdSpawner _parentSpawner;

    private void Awake()
    {
        BirdHealth birdHealth = gameObject.GetComponent<BirdHealth>();
        if (birdHealth)
        {
            birdHealth.SubscribeToOnDeath(BirdHealth_OnDeath);
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find BirdHealth");
    }

    public void Initialize(BirdSpawner birdSpawner)
    {
        _parentSpawner = birdSpawner;
    }

    /// <summary>
    /// If bird is dead, it disables gameobject. If not, it enables the gameobject.
    /// </summary>
    /// <param name="isDead"></param>
    private void BirdHealth_OnDeath(bool isDead)
    {
        gameObject.SetActive(!isDead);

        if(isDead)
            _parentSpawner.AddObjectToObjectPool(gameObject);
    }

    private void OnBecameInvisible()
    {
        _parentSpawner.AddObjectToObjectPool(gameObject);
    }
}
