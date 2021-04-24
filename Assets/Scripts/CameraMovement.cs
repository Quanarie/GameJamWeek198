using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject cat;
    public Vector3 offset;

    void FixedUpdate()
    {
        if (cat.activeSelf)
        {
            transform.position = Vector3.Lerp(transform.position, cat.transform.position + offset, 0.032f);
        }
    }
}
