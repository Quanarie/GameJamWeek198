using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to intialize the bird
/// </summary>
public class BirdInitializer : MonoBehaviour
{
    [SerializeField]
    private BirdFlyAI _birdFlyAI;
    public void Initialize(BirdStartData birdStartData)
    {
        gameObject.transform.position = birdStartData.Position;
        gameObject.transform.rotation = birdStartData.Rotation;

        if(!_birdFlyAI)
            _birdFlyAI = gameObject.GetComponent<BirdFlyAI>();

        if (_birdFlyAI)
            _birdFlyAI.Initialize(birdStartData.FlyDirection, 5);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find {typeof(BirdFlyAI).FullName}.");
    }
}
