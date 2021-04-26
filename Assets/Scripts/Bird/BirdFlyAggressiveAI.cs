using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class BirdFlyAggressiveAI : BirdFlyAI
{
    [SerializeField]
    private CircleCollider2D _aggroRange;

    [SerializeField]
    private float _aggressiveFlySpeed;

    [SerializeField]
    private float _minDistanceFromTarget;

    [SerializeField]
    private bool _isAttackInitiated;

    [SerializeField]
    private Transform _target;

    private void Awake()
    {
        BirdAttackAI birdAttackAI = gameObject.GetComponent<BirdAttackAI>();

        if (birdAttackAI)
            birdAttackAI.SubscribeToOnAttackInitiated(BirdAttackAI_OnAttackInitiated);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find {typeof(BirdAttackAI).FullName}.");

        _aggroRange = GetComponent<CircleCollider2D>();
        if (!_aggroRange)
            _aggroRange = gameObject.AddComponent<CircleCollider2D>();
    }

    public override void Initialize(Direction flyDirection, BirdFlyData birdFlyData)
    {
        _flyDirection = flyDirection;
        _normalFlySpeed = birdFlyData.NormalFlySpeed;
        _aggressiveFlySpeed = birdFlyData.AggressiveFlySpeed;
        _aggroRange.radius = birdFlyData.AggroRange;
        _minDistanceFromTarget = birdFlyData.MinDistanceFromTarget;
        _isAttackInitiated = false;
        ChangeFlyMode(FlyMode.Normal);
        _target = PlayerIdentifier.Current ? PlayerIdentifier.Current.transform : null;
    }

    private void Update()
    {
        Fly(_flyMode);
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

                if (!_isAttackInitiated)
                {
                    if (Vector2.Distance(gameObject.transform.position, _target.position) > _minDistanceFromTarget)
                    {
                        FlyTowardsTarget(_target);
                    }
                }
                
                break;
            default:
                break;
        }
    }

    private void FlyTowardsDirection(Direction flyDirection)
    {
        if (flyDirection == Direction.Left)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.Translate(-gameObject.transform.right * Time.deltaTime * _normalFlySpeed);
        }
        else if (flyDirection == Direction.Right)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.Translate(gameObject.transform.right * Time.deltaTime * _normalFlySpeed);
        }
        else
            Debug.LogError($"{GetType().FullName} : Invalid fly direction.");
    }

    private void FlyTowardsTarget(Transform target)
    {
        if (target)
        {
            if (target.position.x > gameObject.transform.position.x)
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.position, Time.deltaTime * _aggressiveFlySpeed);
        }
        else
            Debug.LogError($"{GetType().FullName} : Target is missing.");
    }



    private void BirdAttackAI_OnAttackInitiated(bool isAttackInitiated) => _isAttackInitiated = isAttackInitiated;

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target)
        {
            if(collision.gameObject == _target.gameObject)
            {
                ChangeFlyMode(FlyMode.Attack);
            }
        }
        else
            Debug.LogError($"{GetType().FullName} : Target is missing.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_target)
        {
            if (collision.gameObject == _target.gameObject)
            {
                ChangeFlyMode(FlyMode.Normal);
            }
        }
        else
            Debug.LogError($"{GetType().FullName} : Target is missing.");
    }

    #endregion

}
