using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteSound : SoundPlayer
{
    [SerializeField]
    private string _parachuteOnID;

    [SerializeField]
    private string _parachuteOffID;

    private void Start()
    {
        Parachute parachute = GetComponent<Parachute>();
        parachute.SubscribeToOnParachuteModeChanged(Parachute_OnParachuteModeChanged);
    }

    private void Parachute_OnParachuteModeChanged(ParachuteMode parachuteMode)
    {
        if (parachuteMode == ParachuteMode.Close)
            PlaySound(_parachuteOffID);
        else
            PlaySound(_parachuteOnID);
    }
}
