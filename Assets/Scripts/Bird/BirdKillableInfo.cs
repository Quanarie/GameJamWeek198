using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdKillableInfo : MonoBehaviour
{
    [SerializeField]
    private BirdKillType _birdType;

    public BirdKillType GetBirdKillType() => _birdType;
}
