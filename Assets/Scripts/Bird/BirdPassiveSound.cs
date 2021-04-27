using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPassiveSound : BirdSound
{
    private void Update()
    {
        if(_isVisibleInCamera)
            PlayNormalSoundAtRegularIntervals();
    }
}
