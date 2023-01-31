using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullHealth : Health
{
    [HideInInspector] public ShipShield shipShield = null;

    public override bool TakeDamage(float damage)
    {
        if (shipShield == null || shipShield.overloadRecharge)
        {
            currentHealth -= damage;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
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
                currentHealth += shipShield.currentShield;

                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

                shipShield.currentShield = 0;

                shipShield.ShieldOverload();

                if (currentHealth <= 0)
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
        currentHealth += repairPoints;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
