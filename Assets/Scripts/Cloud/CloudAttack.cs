using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(PlayerIdentifier.Current == collision.gameObject)
        {
            CatHealth catHealth = PlayerIdentifier.Current.GetComponent<CatHealth>();
            catHealth.ReduceHealth(_attackDamage);
        }
    }
}
