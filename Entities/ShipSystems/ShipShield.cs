using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : MonoBehaviour
{
    public ShipDataSO shipData;

    public bool overloadRecharge = false;
    private bool standardRecharge = false;

    public float currentShield;

    void Start()
    {
        currentShield = shipData.maxShield;
    }

    public void ShieldOverload()
    {
        StopAllCoroutines();
        
        overloadRecharge = true;
        standardRecharge = false;

        StartCoroutine(OverloadRecovery());
    }

    public void CheckForShieldRecharge()
    {
        if (!standardRecharge)
        {
            standardRecharge = true;
            StartCoroutine(StandardRecovery());
        }
    }

    IEnumerator OverloadRecovery()
    {
        while (currentShield < shipData.maxShield)
        {
            currentShield += shipData.shieldRecoveryRate;
            yield return new WaitForSeconds(1);
        }

        currentShield = Mathf.Clamp(currentShield, 0, shipData.maxShield);
        overloadRecharge = false;
        yield break;
    }

    IEnumerator StandardRecovery()
    {
        while (currentShield < shipData.maxShield)
        {
            currentShield += shipData.shieldRecoveryRate;
            yield return new WaitForSeconds(1);
        }

        currentShield = Mathf.Clamp(currentShield, 0, shipData.maxShield);
        standardRecharge = false;
        yield break;
    }
}
