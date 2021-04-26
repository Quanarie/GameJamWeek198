using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEliminator : MonoBehaviour
{
    /// <summary>
    /// This script deletes the cloud if it is outside camera view
    /// </summary>

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
