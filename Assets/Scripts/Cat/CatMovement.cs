using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float maxSpeed;
    public float forceValue;
    public GameObject parachute;

    private Rigidbody catRB;

    void Start()
    {
        catRB = GetComponent<Rigidbody>();

        parachute.GetComponent<Parachute>().parachuteEventOpen += ParachuteOn;
        parachute.GetComponent<Parachute>().parachuteEventClose += ParachuteOff;
    }
    void ParachuteOn()
    {
        parachute.SetActive(true);
    }
    void ParachuteOff()
    {
        parachute.SetActive(false);
    }
    void FixedUpdate()
    {
        UpdateHorizontalMove();
        ClampOnMaxSpeed();
    }
    void UpdateHorizontalMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (catRB.velocity.x < maxSpeed)
            {
                catRB.AddForce(forceValue, 0f, 0f);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (catRB.velocity.x > -maxSpeed)
            {
                catRB.AddForce(-forceValue, 0f, 0f);
            }
        }
    }
    void ClampOnMaxSpeed()
    {
        if (!Input.GetKey(KeyCode.D))
        {
            if (catRB.velocity.x > 0f)
            {
                catRB.AddForce(Vector3.right * -catRB.velocity.x);
            }
        }
        if (!Input.GetKey(KeyCode.A))
        {
            if (catRB.velocity.x < 0f)
            {
                catRB.AddForce(Vector3.right * -catRB.velocity.x);
            }
        }
    }
}
