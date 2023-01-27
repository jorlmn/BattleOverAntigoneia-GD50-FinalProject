using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealth : Health
{
    private float maxWeaponHealth;
    private float currentWeaponHealth;
    private ShipHullHealth shipHull;
    private Weapon weapon;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        shipHull = GetComponentInParent<ShipHullHealth>();
        maxWeaponHealth = weapon.weaponData.maxHealth;
        currentWeaponHealth = maxWeaponHealth;
    }

    public override void TakeDamage(float damage)
    {
        currentWeaponHealth -= damage;

        Debug.Log("Turret took damage " + damage);


        shipHull.TakeDamage((damage / 4) + (currentWeaponHealth < 0 ? Mathf.Abs(currentWeaponHealth) : 0));

        currentWeaponHealth = Mathf.Clamp(currentWeaponHealth, 0, maxWeaponHealth);

        if (currentWeaponHealth <= 0)
        {
            weapon.active = false;
        }
    }

    public override void Repair(float repairPoints)
    {
        currentWeaponHealth += repairPoints;
        currentWeaponHealth = Mathf.Clamp(currentWeaponHealth, 0, maxWeaponHealth);
    }
}
