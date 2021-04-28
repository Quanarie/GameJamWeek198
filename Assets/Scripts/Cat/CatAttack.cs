using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatAttack : MonoBehaviour
{
    [SerializeField,Header("Set in Inspector")]
    private float _attackRadius;

    [SerializeField]
    private float _attackDamage;

    [SerializeField]
    private CircleCollider2D _attackRangeCollider;

    [SerializeField]
    private Parachute _parachute;
    private ParachuteMode _parachuteMode;

    private List<GameObject> _birdsInRange = new List<GameObject>();
    private List<GameObject> _objectsToIgnore = new List<GameObject>();

    [SerializeField]
    private float _attackDuraction;

    [SerializeField]
    private bool _isAbleToAttemptToAttack;

    private Coroutine _attackInitiatedCoroutine;

    private event UnityAction<bool> OnAttackInitiated;
    void Start()
    {
        if (!_parachute)
        {
            _parachute = GetComponent<Parachute>();
            if (!_parachute)
            {
                _parachute = GetComponentInChildren<Parachute>();
                _parachute.SubscribeToOnParachuteModeChanged(Parachute_OnParachuteModeChanged);
            }

            else
                Debug.LogError($"{GetType().FullName} : Failed to find Parachute.");
        }
        else
            _parachute.SubscribeToOnParachuteModeChanged(Parachute_OnParachuteModeChanged);

        if (_attackRangeCollider)
            _attackRangeCollider.radius = _attackRadius;
        else
            Debug.LogError($"{GetType().FullName} : AttackRangeCollider is missing.");
    }
    //void Update()
    //{
    //    if (_parachuteMode == ParachuteMode.Close && Input.GetKey(KeyCode.S))
    //    {
    //        if (_isAbleToAttemptToAttack)
    //        {
    //            if (_attackInitiatedCoroutine == null)
    //                _attackInitiatedCoroutine = StartCoroutine(AttackInitiated());
    //            else
    //                Debug.LogError($"{GetType().FullName} : Attack Coroutine is not null.");
    //        }
    //    }
    //}

    private void Parachute_OnParachuteModeChanged(ParachuteMode parachuteMode) => _parachuteMode = parachuteMode;

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

        while (timePassed < _attackDuraction)
        {
            timePassed += Time.deltaTime;
            yield return waitForEndOfFrame;
        }

        //AttackTarget();

        _isAbleToAttemptToAttack = true;
        OnAttackInitiated?.Invoke(false);

        _attackInitiatedCoroutine = null;
    }

    private void AttackTarget(GameObject objBird)
    {
        //if(_birdsInRange.Count > 0)
        //{
        //    GameObject objBird = _birdsInRange[UnityEngine.Random.Range(0, Mathf.Max(_birdsInRange.Count - 1, 0))];

        //    if (objBird)
        //    {
        //        BirdHealth birdHealth = objBird.GetComponent<BirdHealth>();
        //        birdHealth.ReduceHealth(_attackDamage);

        //        //This is a hotfix to remove bird instantly. Remove the line below once a better solution is found.
        //        _birdsInRange.Remove(objBird);
        //    }
        //    else
        //        Debug.LogError($"{GetType().FullName} : Failed to find a bird.");
        //}

        BirdHealth birdHealth = objBird.GetComponent<BirdHealth>();
        birdHealth.ReduceHealth(_attackDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_objectsToIgnore.Contains(other.gameObject))
        {
            BirdKillableInfo birdKillableInfo = other.gameObject.GetComponent<BirdKillableInfo>();

            if (birdKillableInfo)
            {
                if (birdKillableInfo.GetBirdKillType() == BirdKillType.Killable)
                {
                    AttackTarget(birdKillableInfo.gameObject);
                }else
                    _objectsToIgnore.Add(other.gameObject);
            }
            else
                _objectsToIgnore.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_objectsToIgnore.Contains(other.gameObject))
        {
            if (_birdsInRange.Contains(other.gameObject))
                _birdsInRange.Remove(other.gameObject);
        }
    }

    #region Event Subscription
    public void SubscribeToOnAttackInitiated(UnityAction<bool> callback) => HelperUtility.SubscribeTo(ref OnAttackInitiated, ref callback);
    public void UnsubscribeFromOnAttackInitiated(UnityAction<bool> callback) => HelperUtility.UnsubscribeFrom(ref OnAttackInitiated, ref callback);
    #endregion
}
