using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineHealth : Health
{
    private float maxEngineHealth;
    private float currentEngineHealth;

    private ShipSystemsManager shipSystems;

    void Start()
    {
        shipSystems = GetComponentInParent<ShipSystemsManager>();
        maxEngineHealth = shipSystems.shipData.maxEngineHealth;
        currentEngineHealth = maxEngineHealth;
    }

    public override void TakeDamage(float damage)
    {
        currentEngineHealth -= damage;

        shipSystems.shipHealth.TakeDamage((damage / 4) + (currentEngineHealth < 0 ? Mathf.Abs(currentEngineHealth) : 0));

        currentEngineHealth = Mathf.Clamp(currentEngineHealth, 0, maxEngineHealth);

        shipSystems.shipEngine.EngineSpeedAdjustment(currentEngineHealth / maxEngineHealth);
    }

    public override void Repair(float repairPoints)
    {
        currentEngineHealth += repairPoints;
        currentEngineHealth = Mathf.Clamp(currentEngineHealth, 0, maxEngineHealth);

        shipSystems.shipEngine.EngineSpeedAdjustment(currentEngineHealth / maxEngineHealth);
    }
}
