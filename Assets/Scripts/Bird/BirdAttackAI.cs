using System.Collections;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// This script controls the behaviour of attacks done by bird
/// </summary>
public class BirdAttackAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    private AttackMode _currentAttackMode;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private float _attackDamage;

    [SerializeField]
    private float _attackDuraction;

    [SerializeField]
    private bool _isAbleToAttemptToAttack;

    public event UnityAction<bool> OnAttackInitiated;

    private void Awake()
    {
        BirdFlyAI birdFlyAI = gameObject.GetComponent<BirdFlyAI>();

        if (birdFlyAI)
        {
            birdFlyAI.SubscribeToOnFlyModeChanged(BirdFlyAI_OnFlyModeChanged);
            _currentAttackMode = birdFlyAI.GetCurrentFlyMode() == FlyMode.Attack ? AttackMode.Attack : AttackMode.Idle;
        }
            
        else
            Debug.LogError($"{GetType().FullName} : Failed to find {typeof(BirdFlyAI).FullName}.");
    }

  

    public void Initialize(BirdAttackData birdAttackData)
    {
        _attackRange = birdAttackData.AttackRange;
        _attackDamage = birdAttackData.AttackDamage;
        _attackDuraction = birdAttackData.AttackDuration;
        _isAbleToAttemptToAttack = true;
        _target = PlayerIdentifier.Current ? PlayerIdentifier.Current.gameObject : null;
        if (!_target)
            Debug.LogError($"{GetType().FullName} : Failed to find Target.");
    }

    private void Update()
    {
        if(_currentAttackMode == AttackMode.Attack)
        {
            if (_target)
            {
                if (_isAbleToAttemptToAttack)
                {
                    if (Vector2.Distance(_target.transform.position, gameObject.transform.position) <= _attackRange)
                    {
                        StartCoroutine(AttackInitiated());
                    }
                }
            }
        }
    }

    /// <summary>
    /// This corroutine controls the behavior of how attack is done.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackInitiated()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        _isAbleToAttemptToAttack = false;
        OnAttackInitiated?.Invoke(true);
        float timePassed = 0;

        while(timePassed < _attackDuraction)
        {
            timePassed += Time.deltaTime;
            yield return waitForEndOfFrame;
        }

        AttackTarget();

        _isAbleToAttemptToAttack = true;
        yield return new WaitForSeconds(1);

        OnAttackInitiated?.Invoke(false);
    }

    /// <summary>
    /// This function first checks if the target is within the attack range. If it is, then it will deal damage to the target.
    /// </summary>
    private void AttackTarget()
    {
        if(Vector2.Distance(_target.transform.position, gameObject.transform.position) <= _attackRange)
        {
            CatHealth catHealth = _target.GetComponent<CatHealth>();
            catHealth.ReduceHealth(_attackDamage);
        }

        Debug.LogError($"{GetType().FullName} : Animation missing.");
    }

    /// <summary>
    /// When the flyMode is changed in BirdFlyAI, the attack mode changes based on that.
    /// </summary>
    /// <param name="flyMode"></param>
    private void BirdFlyAI_OnFlyModeChanged(FlyMode flyMode)
    {
        if(flyMode == FlyMode.Attack)
        {
            _currentAttackMode = AttackMode.Attack;
        }
        else
        {
            _currentAttackMode = AttackMode.Idle;
        }
    }

    #region Event Subscription
    public void SubscribeToOnAttackInitiated(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnAttackInitiated, ref callback);
    public void UnsubscribeFromOnAttackInitiated(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnAttackInitiated, ref callback);
    #endregion
}
