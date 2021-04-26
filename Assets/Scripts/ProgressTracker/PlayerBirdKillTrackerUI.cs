using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBirdKillTrackerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _txtKillCount;

    private void Start()
    {
        PlayerBirdKillTracker.Current.SubscribeToOnKillCountChanged(PlayerBirdKillTracker_OnKillCountChanged);
        _txtKillCount.text = PlayerBirdKillTracker.Current.GetCurrentKillCount().ToString();
    }

    private void PlayerBirdKillTracker_OnKillCountChanged(int count)
    {
        if (_txtKillCount)
        {
            _txtKillCount.text = count.ToString();
        }
        else
            Debug.LogError($"{GetType().FullName} : Failed to find TxtKillCount.");
    }
}
