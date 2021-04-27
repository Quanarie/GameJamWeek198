using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class used to intialize the bird
/// </summary>
public abstract class BirdInitializer : MonoBehaviour
{
    [SerializeField]
    protected BirdData _birdData;

    [SerializeField]
    private BirdFlyAI _birdFlyAI;

    [SerializeField]
    private BirdHealth _birdHealth;

    [SerializeField]
    private BirdSound _birdSound;

    public virtual void Initialize(BirdStartData birdStartData)
    {
        gameObject.transform.position = birdStartData.Position;
        gameObject.transform.rotation = birdStartData.Rotation;

        if (!_birdFlyAI)
            _birdFlyAI = gameObject.GetComponent<BirdFlyAI>();

        if (_birdFlyAI)
            _birdFlyAI.Initialize(birdStartData.FlyDirection, _birdData.BirdFlyData);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find {typeof(BirdFlyAI).FullName}.");

        if (!_birdHealth)
            _birdHealth = gameObject.GetComponent<BirdHealth>();

        if (_birdHealth)
            _birdHealth.Initialize(_birdData.BirdHealthData);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find {typeof(BirdHealth).FullName}.");

        if (!_birdSound)
            _birdSound = gameObject.GetComponent<BirdSound>();
			
		if(_birdSound)
			_birdSound.Initialize();
		else
            Debug.LogError($"{GetType().FullName} : Failed to find {typeof(BirdSound).FullName}.");

    }
}
