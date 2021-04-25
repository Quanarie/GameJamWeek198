using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEliminator : MonoBehaviour
{
    /// <summary>
    /// This script deletes all clouds from CloudSpawner.Clouds that are out of camera range
    /// </summary>
    
    [Header("Set in Inspector")]
    public float maxYCoordinate;

    void Update()
    {
        DeleteClouds();
    }
    void DeleteClouds()
    {
        List<GameObject> Clouds = GetComponentInParent<CloudSpawner>().Clouds;
        for (int i = 0; i < Clouds.Count; i++)
        {
            if (Clouds[i].transform.position.y > maxYCoordinate)
            {
                Destroy(Clouds[i]);
                Clouds.Remove(Clouds[i]);
            }
        }
    }
}
