using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHullHealth : Health
{
    public float maxHullHealth;

    public float currentHullHealth;

    public override void TakeDamage(float damage)
    {
        currentHullHealth -= damage;

        currentHullHealth = Mathf.Clamp(currentHullHealth, 0, maxHullHealth);

        Debug.Log("Hull took damage " + damage);

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
