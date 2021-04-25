using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    /// <summary>
    /// This script moves all clouds from CloudSpawner.Clouds upper
    /// </summary>
    
    [Header("Set in Inspector")]
    public float cloudSpeed;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += Time.deltaTime * cloudSpeed;
        transform.position = pos;
    }
}
