using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineHealth : Health
{
    private float maxEngineHealth;
    private float currentEngineHealth;
    private ShipSystemsManager shipSystems;

    [SerializeField] List<GameObject> engineParts = new ();
    private List<int> activeEnginesIndex = new();

    void Start()
    {
        shipSystems = GetComponentInParent<ShipSystemsManager>();
        maxEngineHealth = shipSystems.shipData.maxEngineHealth;
        currentEngineHealth = maxEngineHealth;

        for(int i = 0; i < engineParts.Count; i++)
        {
            activeEnginesIndex.Add(i);
        }
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
            case float i when i > 0.5f:
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

                int enginesToDisable = Mathf.RoundToInt(Mathf.Lerp(0, engineParts.Count, 1 - healthPercentage));

                while (enginesToDisable > engineParts.Count - activeEnginesIndex.Count)
                {
                    int randomEngineToDisable = Random.Range(0, activeEnginesIndex.Count - 1);

                    engineParts[activeEnginesIndex[randomEngineToDisable]].SetActive(false);
                    activeEnginesIndex.Remove(randomEngineToDisable);

                    enginesToDisable--;
                }

                break;
        }
    }  
}
