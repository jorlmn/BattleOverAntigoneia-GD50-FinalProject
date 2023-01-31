using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineHealth : Health
{
    private ShipSystemsManager shipSystems;

    [SerializeField] List<GameObject> engineParts = new ();
    private List<int> activeEnginesIndex = new();

    [SerializeField] ParticleSystem explosionAndSmoke;

    void Start()
    {
        shipSystems = GetComponentInParent<ShipSystemsManager>();
        maxHealth = shipSystems.shipData.maxEngineHealth;
        currentHealth = maxHealth;

        explosionAndSmoke.gameObject.SetActive(false);

        for(int i = 0; i < engineParts.Count; i++)
        {
            activeEnginesIndex.Add(i);
        }
    }

    public override bool TakeDamage(float damage)
    {
        bool hitHull = shipSystems.shipHealth.TakeDamage((damage / 4) + (currentHealth - damage < 0 ? Mathf.Abs(currentHealth - damage) : 0));

        if (hitHull == true)
        {
            currentHealth -= damage;

            currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);

            shipSystems.shipEngine.EngineSpeedAdjustment(currentHealth / maxHealth);

            DisableEngineParts(currentHealth / maxHealth);

            if (currentHealth <= 0)
            {
                explosionAndSmoke.gameObject.SetActive(true);
                shipSystems.shipEngine.TurboOff();
                shipSystems.shipEngine.active = false;
            }
        }

        return hitHull;
    }

    public override void Repair(float repairPoints)
    {
        currentHealth += repairPoints;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        shipSystems.shipEngine.EngineSpeedAdjustment(currentHealth / maxHealth);
    }

    void DisableEngineParts(float healthPercentage)
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
