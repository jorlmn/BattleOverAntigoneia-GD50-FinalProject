using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullHealth : Health
{
    public float maxHullHealth;

    public float currentHullHealth;

    public override void TakeDamage(float damage)
    {
        currentHullHealth -= damage;

        currentHullHealth = Mathf.Clamp(currentHullHealth, 0, maxHullHealth);

        if (currentHullHealth <= 0)
        {
            //OnDeath(this, EventArgs.Empty);
        }
    }

    public override void Repair(float repairPoints)
    {
        currentHullHealth += repairPoints;
        currentHullHealth = Mathf.Clamp(currentHullHealth, 0, maxHullHealth);
    }
}
