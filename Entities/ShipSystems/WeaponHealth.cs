using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealth : Health
{
    private float maxWeaponHealth;
    private float currentWeaponHealth;
    private HullHealth shipHull;
    private Weapon weapon;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        shipHull = GetComponentInParent<HullHealth>();
        maxWeaponHealth = weapon.weaponData.maxHealth;
        currentWeaponHealth = maxWeaponHealth;
    }

    public override bool TakeDamage(float damage)
    {

        bool hitHull = shipHull.TakeDamage((damage / 4) + (currentWeaponHealth - damage < 0 ? Mathf.Abs(currentWeaponHealth - damage) : 0));

        if (hitHull == true)
        {
            currentWeaponHealth -= damage;

            currentWeaponHealth = Mathf.Clamp(currentWeaponHealth, 0, maxWeaponHealth);

            if (currentWeaponHealth <= 0)
            {
                weapon.active = false;
            }
        }

        return hitHull;
    }

    public override void Repair(float repairPoints)
    {
        currentWeaponHealth += repairPoints;
        currentWeaponHealth = Mathf.Clamp(currentWeaponHealth, 0, maxWeaponHealth);
    }
}
