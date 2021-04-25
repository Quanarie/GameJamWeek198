using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    /// <summary>
    /// This class delegates opening and closing the parachute to the cat movement script
    /// </summary>
    
    public delegate void ParachuteDelegateOpen();
    public event ParachuteDelegateOpen parachuteEventOpen;

    public delegate void ParachuteDelegateClose();
    public event ParachuteDelegateClose parachuteEventClose;

    private bool isOpen = false;
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (parachuteEventOpen != null && !isOpen)
            {
                parachuteEventOpen?.Invoke();
                isOpen = true;
            }
            else if (parachuteEventClose != null && isOpen)
            { 
                parachuteEventClose?.Invoke();
                isOpen = false;
            }
                
        }
    }
}