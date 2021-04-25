using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for initializing aggressive birds
/// </summary>
public class BirdAggressiveInitializer : BirdInitializer
{
    [SerializeField]
    private BirdAttackAI _birdAttackAI;
    public override void Initialize(BirdStartData birdStartData)
    {
        base.Initialize(birdStartData);

        if (!_birdAttackAI)
            _birdAttackAI = GetComponent<BirdAttackAI>();

        if (_birdAttackAI)
            _birdAttackAI.Initialize(_birdData.BirdAttackData);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find Bird Attack AI.");
    }
}
