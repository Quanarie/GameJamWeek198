using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float attackRadius;
    public LayerMask birdLayerMask;

    [SerializeField]
    private Parachute _parachute;
    private ParachuteMode _parachuteMode;

    void Start()
    {
        if (!_parachute)
        {
            _parachute = GetComponent<Parachute>();
            if (!_parachute)
            {
                _parachute = GetComponentInChildren<Parachute>();
                _parachute.SubscribeToOnParachuteModeChanged(Parachute_OnParachuteModeChanged);
            }

            else
                Debug.LogError($"{GetType().FullName} : Failed to find Parachute.");
        }
        _parachute.SubscribeToOnParachuteModeChanged(Parachute_OnParachuteModeChanged);
    }
    void FixedUpdate()
    {
        if (_parachuteMode == ParachuteMode.Close && Input.GetKey(KeyCode.S))
        {
            GameObject target = FindClosestBirdUnderCat();
            if (target != null)
            {
                Destroy(target);
                
                //add health
            }
        }
    }
    GameObject FindClosestBirdUnderCat()
    {
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        GameObject closestBird = null;
        foreach (GameObject bird in birds)
        {
            Vector3 catPos = transform.position;
            Vector3 birdPos = bird.transform.position;
            if (catPos.y - birdPos.y < attackRadius && catPos.y - birdPos.y > 0 && Mathf.Abs(catPos.x - birdPos.x) < attackRadius)
            {
                if (closestBird != null)
                {
                    if (Mathf.Abs(birdPos.y - catPos.y) < Mathf.Abs(closestBird.transform.position.y - catPos.y))
                    {
                        closestBird = bird;
                    }
                }
                else
                {
                    closestBird = bird;
                }
            }
        }
        return closestBird;
    }

    private void Parachute_OnParachuteModeChanged(ParachuteMode parachuteMode) => _parachuteMode = parachuteMode;
}
