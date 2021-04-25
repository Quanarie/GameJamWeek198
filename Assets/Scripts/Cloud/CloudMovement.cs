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
        MoveClouds();
    }
    void MoveClouds()
    {
        foreach (GameObject cloud in GetComponentInParent<CloudSpawner>().Clouds)
        {
            Vector3 pos = cloud.transform.position;
            pos.y += Time.deltaTime * cloudSpeed;
            cloud.transform.position = pos;
        }
    }
}
