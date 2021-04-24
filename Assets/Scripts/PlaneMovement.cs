using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{    
    [Header("Set in Inspector")]
    public GameObject cat;
    public float speed;
    
    private bool isCatDropped = false;
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        if (pos.x > -20 && isCatDropped == false)
        {
            DropTheCat();
            isCatDropped = true;
        }
        else if (pos.x > 50) Destroy(transform.gameObject);
    }
    void DropTheCat()
    {
        cat.SetActive(true);
        cat.transform.SetParent(null);
    }
}
