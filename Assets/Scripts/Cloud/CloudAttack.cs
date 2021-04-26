using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackDamage;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (PlayerIdentifier.Current.gameObject == collision.gameObject)
    //    {
    //        CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
    //        catHealth.ReduceHealth(_attackDamage);
    //        Debug.LogError($"Collider");
    //    }
    //}

    /// <summary>
    /// This is a temporary fix for cloud attack. This function should be removed at a later stage to respect cat's attack range trigger and cat's collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerIdentifier.Current.gameObject == collision.gameObject)
        {
            CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
            catHealth.ReduceHealth(_attackDamage);
            Debug.LogError($"Trigger");
        }
    }
}
