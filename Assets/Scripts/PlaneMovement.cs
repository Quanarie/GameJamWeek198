using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{    
    [Header("Set in inspector")]
    public float speed;
    public GameObject cat;
    
    private bool isCatDropped = false;
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        if (pos.x > -15 && isCatDropped == false)
        {
            DropTheCat();
            isCatDropped = true;
        }
    }
    void DropTheCat()
    {
        cat.SetActive(true);
        cat.transform.SetParent(null);
    }
}
