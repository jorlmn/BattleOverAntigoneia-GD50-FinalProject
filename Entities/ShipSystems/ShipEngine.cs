using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    [HideInInspector] public ShipDataSO shipData;

    public string currentVelocity;
    public Dictionary<string, float> shipVelocities = new ();


    private float turboRecoveryRate;
    private float turboSpendRate;
    private int maxTurbo = 100;
    public float currentTurbo = 100;
    private bool overloadRecharge = false;
    void Start()
    {
        turboRecoveryRate = shipData.turboRecoveryRate;
        turboSpendRate = shipData.turboSpendRate;

        shipVelocities["standard"] = shipData.standardVelocity;
        shipVelocities["turbo"] = shipData.turboVelocity;
        shipVelocities["stealth"] = shipData.stealthVelocity;
        shipVelocities["rotation"] = shipData.rotationSpeed;

        currentVelocity = "standard";
    }

    public void EngineSpeedAdjustment(float healthPercentage)
    {
        shipVelocities["standard"] = Mathf.Lerp(1, shipData.standardVelocity, healthPercentage);
        shipVelocities["turbo"] = Mathf.Lerp(1, shipData.turboVelocity, healthPercentage);
        shipVelocities["stealth"] = Mathf.Lerp(1, shipData.stealthVelocity, healthPercentage);
        shipVelocities["rotation"] = Mathf.Lerp(1, shipData.rotationSpeed, healthPercentage);
    }

    public void ToggleTurbo()
    {
        if (currentVelocity == "turbo")
        {
            currentVelocity = "standard";
            StopAllCoroutines();
            StartCoroutine(TurboStandardRecovery());
        }
        else
        {
            if (!overloadRecharge)
            {
                currentVelocity = "turbo";
                StopAllCoroutines();
                StartCoroutine(TurboSpending());
            }
        }
    }

    public void ToggleStealth()
    {
        if (currentVelocity == "turbo")
        {
            currentVelocity = "stealth";
            StopAllCoroutines();
            StartCoroutine(TurboStandardRecovery());
        }
        else
        {
            currentVelocity = currentVelocity == "stealth" ? "standard" : "stealth";
        }
    }

    private void TurboOverload()
    {
        StopAllCoroutines();
        currentVelocity = "standard";
        overloadRecharge = true;
        StartCoroutine(TurboOverloadRecovery());
    }

    IEnumerator TurboOverloadRecovery()
    {
        while (currentTurbo < maxTurbo)
        {
            currentTurbo += turboRecoveryRate;
            yield return new WaitForSeconds(1);
        }

        currentTurbo = Mathf.Clamp(currentTurbo, 0, maxTurbo);
        overloadRecharge = false;
        yield break;
    }

    IEnumerator TurboStandardRecovery()
    {
        while (currentTurbo < maxTurbo)
        {
            currentTurbo += turboRecoveryRate;
            yield return new WaitForSeconds(1);
        }

        currentTurbo = Mathf.Clamp(currentTurbo, 0, maxTurbo);
        yield break;
    }

    IEnumerator TurboSpending()
    {
        while (currentTurbo > 0)
        {
            currentTurbo -= turboSpendRate;
            yield return new WaitForSeconds(1);
        }

        currentTurbo = Mathf.Clamp(currentTurbo, 0, maxTurbo);
        TurboOverload();
        yield break;
    }

}
