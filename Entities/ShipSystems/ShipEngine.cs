using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    [HideInInspector] public ShipDataSO shipData;

    public string currentVelocity;
    public Dictionary<string, float> shipVelocities = new ();
    void Start()
    {
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
}
