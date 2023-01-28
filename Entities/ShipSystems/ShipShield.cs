using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : MonoBehaviour
{
    public float maxShield;
    public float currentShield;
    public float shieldRecoveryRate;

    public bool overloadRecharge = false;
    private bool standardRecharge = false;

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
        while (currentShield < maxShield)
        {
            currentShield += shieldRecoveryRate;
            yield return new WaitForSeconds(1);
        }

        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        overloadRecharge = false;
        yield break;
    }

    IEnumerator StandardRecovery()
    {
        while (currentShield < maxShield)
        {
            currentShield += shieldRecoveryRate;
            yield return new WaitForSeconds(1);
        }

        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        standardRecharge = false;
        yield break;
    }
}
