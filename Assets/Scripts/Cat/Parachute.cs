using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    public delegate void ParachuteDelegate();
    public event ParachuteDelegate parachuteEvent;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (parachuteEvent != null)
                parachuteEvent();
        }
    }
}
