using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudShowThunder : MonoBehaviour
{
    [SerializeField]
    private GameObject _objThunder;

    [SerializeField]
    private CloudAttack _cloudAttack;

    private void Start()
    {
        if(!_cloudAttack)
            _cloudAttack = gameObject.GetComponent<CloudAttack>();

        if (_cloudAttack)
            _cloudAttack.SubscribeToOnCloudAttack(CloudAttack_OnCloudAttack);
        else
            Debug.LogError($"{GetType().FullName} : Failed to find CloudAttack.");
    }

    private void CloudAttack_OnCloudAttack()
    {
        StartCoroutine(ShowHideThunder());
    }

    private IEnumerator ShowHideThunder()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        _objThunder.SetActive(true);

        yield return waitForSeconds;

        _objThunder.SetActive(false);
    }
}
