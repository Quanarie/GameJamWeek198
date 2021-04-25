using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    /// <summary>
    /// This class delegates opening and closing the parachute to the cat movement script
    /// </summary>
    
    public delegate void ParachuteDelegateOpen();
    public event ParachuteDelegateOpen ParachuteEventOpen;

    public delegate void ParachuteDelegateClose();
    public event ParachuteDelegateClose ParachuteEventClose;

    private bool isOpen = false;
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (ParachuteEventOpen != null && !isOpen)
            {
                ParachuteEventOpen?.Invoke();
                isOpen = true;
            }
            else if (ParachuteEventClose != null && isOpen)
            { 
                ParachuteEventClose?.Invoke();
                isOpen = false;
            }
                
        }
    }
}