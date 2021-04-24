using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls Flying AI for passive birds.
/// </summary>
public class BirdFlyPassiveAI : BirdFlyAI
{
    [SerializeField]
    private Direction _flyDirection;

    [SerializeField]
    protected FlyMode _flyMode;

    [SerializeField]
    private float _normalFlySpeed;

    private void Update()
    {
        Fly(_flyMode);
    }

    public override void Initialize(Direction flyDirection, float normalFlySpeed)
    {
        _flyDirection = flyDirection;
        _normalFlySpeed = normalFlySpeed;
        _flyMode = FlyMode.Normal;
    }
    protected override void Fly(FlyMode flyMode)
    {
        switch (flyMode)
        {
            case FlyMode.Idle:

                break;
            case FlyMode.Normal:
                FlyTowardsDirection(_flyDirection);
                break;
            case FlyMode.Attack:
                Debug.LogError($"{GetType().FullName} : Passive Fly AI attempted to do attack flymode.");
                break;
        }
    }

    private void FlyTowardsDirection(Direction flyDirection)
    {
        if (flyDirection == Direction.Left)
            gameObject.transform.Translate(-gameObject.transform.right * Time.deltaTime * _normalFlySpeed);
        else if (flyDirection == Direction.Right)
            gameObject.transform.Translate(gameObject.transform.right * Time.deltaTime * _normalFlySpeed);
        else
            Debug.LogError($"{GetType().FullName} : Invalid fly direction.");
    }

    private void OnBecameInvisible()
    {
        _flyMode = FlyMode.Idle;
    }
}
