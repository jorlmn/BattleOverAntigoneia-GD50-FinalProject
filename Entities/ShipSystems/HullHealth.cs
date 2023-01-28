using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullHealth : Health
{
    public float maxHullHealth;

    public float currentHullHealth;

    [HideInInspector] public ShipShield shipShield = null;

    public override bool TakeDamage(float damage)
    {
        if (shipShield == null || shipShield.overloadRecharge)
        {
            currentHullHealth -= damage;

            currentHullHealth = Mathf.Clamp(currentHullHealth, 0, maxHullHealth);

            if (currentHullHealth <= 0)
            {
                //OnDeath(this, EventArgs.Empty);
            }
            return true;

        }
        else
        {
            shipShield.currentShield -= damage;

            if (shipShield.currentShield <= 0)
            {
                currentHullHealth += shipShield.currentShield;

                currentHullHealth = Mathf.Clamp(currentHullHealth, 0, maxHullHealth);

                shipShield.currentShield = 0;

                shipShield.ShieldOverload();

                if (currentHullHealth <= 0)
                {
                    //OnDeath(this, EventArgs.Empty);
                }
                return true;
            }
            else
            {
                shipShield.CheckForShieldRecharge();
            }
            return false;
        }
    }

    public override void Repair(float repairPoints)
    {
        currentHullHealth += repairPoints;
        currentHullHealth = Mathf.Clamp(currentHullHealth, 0, maxHullHealth);
    }
}
