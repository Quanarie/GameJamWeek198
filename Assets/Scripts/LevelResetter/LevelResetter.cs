using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    public static LevelResetter Current;

    [SerializeField]
    private List<BirdSpawner> _birdSpawners = new List<BirdSpawner>();

    [SerializeField]
    private CameraMovement _cameraMovement;

    private void Awake()
    {
        Current = this;
    }

    public void ResetLevel()
    {
        _birdSpawners.ForEach(c => c.ResetBirdSpawner());
        PlayerProgressTracker.Current.ResetPlayerProgress();
        PlayerBirdKillTracker.Current.ResetPlayerKillProgress();

        _cameraMovement.ResetCameraPosition();
        CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
        catHealth.ResetCatHealth();
        CatMovement catMovement = PlayerIdentifier.Current.GetComponent<CatMovement>();
        catMovement.ResetCatMovement();
    }
}
