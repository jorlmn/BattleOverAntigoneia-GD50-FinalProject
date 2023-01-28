using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineHealth : Health
{
    private float maxEngineHealth;
    private float currentEngineHealth;

    [SerializeField] List<GameObject> engineParts = new ();
    private ShipSystemsManager shipSystems;

    void Start()
    {
        shipSystems = GetComponentInParent<ShipSystemsManager>();
        maxEngineHealth = shipSystems.shipData.maxEngineHealth;
        currentEngineHealth = maxEngineHealth;
    }

    public override bool TakeDamage(float damage)
    {
        bool hitHull = shipSystems.shipHealth.TakeDamage((damage / 4) + (currentEngineHealth - damage < 0 ? Mathf.Abs(currentEngineHealth - damage) : 0));

        if (hitHull == true)
        {
            currentEngineHealth -= damage;

            currentEngineHealth = Mathf.Clamp(currentEngineHealth, 0, currentEngineHealth);

            shipSystems.shipEngine.EngineSpeedAdjustment(currentEngineHealth / maxEngineHealth);

            EnableDisableEngineParts(currentEngineHealth / maxEngineHealth);
        }

        return hitHull;
    }

    public override void Repair(float repairPoints)
    {
        currentEngineHealth += repairPoints;
        currentEngineHealth = Mathf.Clamp(currentEngineHealth, 0, maxEngineHealth);

        shipSystems.shipEngine.EngineSpeedAdjustment(currentEngineHealth / maxEngineHealth);

        EnableDisableEngineParts(currentEngineHealth / maxEngineHealth);
    }

    void EnableDisableEngineParts(float healthPercentage)
    {
        switch (healthPercentage)
        {
            case float i when i > 0.7f:
                foreach(GameObject engine in engineParts)
                {
                    engine.SetActive(true);
                }
                break;

            case float i when i <= 0:
                foreach(GameObject engine in engineParts)
                {
                    engine.SetActive(false);
                }
                break;

            case float i when i <= 0.5f:

                int enginesToDisable = Mathf.RoundToInt((1f / engineParts.Count) / healthPercentage);

                for (int z = 0; z < engineParts.Count; z++)
                {
                    if (enginesToDisable > 0)
                    {   
                        engineParts[z].SetActive(false);
                        enginesToDisable -= 1;
                    }
                    else
                    {
                        engineParts[z].SetActive(true);
                    }
                }
                break;
        }
    }  
}
