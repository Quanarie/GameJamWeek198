using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIntersectionWithClouds : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float thunderDamage;
    public float indestructibilityTime;

    private bool isBlinking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ThunderCloud")
        {
            //GetComponentInParent<CatData>().CatHealthData.maxHealth -= thunderDamage;

            //shock animation ;-;

            StartCoroutine(Blink());
        }
    }
    private void Update()
    {
        if (isBlinking)
        {
            //change color
        }
        else
        {
            //make initial color
        }
    }

    IEnumerator Blink()
    {
        isBlinking = true;
        yield return new WaitForSeconds(indestructibilityTime);
        isBlinking = false;
    }
}
