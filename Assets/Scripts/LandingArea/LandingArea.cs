using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandingArea : MonoBehaviour
{
    public static LandingArea Current;

    private event UnityAction OnLanded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerIdentifier.Current.gameObject)
        {
            OnLanded?.Invoke();
        }
    }
}
