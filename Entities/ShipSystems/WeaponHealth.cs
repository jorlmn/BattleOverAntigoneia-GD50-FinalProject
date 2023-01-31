using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealth : Health
{
    private HullHealth shipHull;
    private Weapon weapon;

    [SerializeField] ParticleSystem explosionAndSmoke;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        shipHull = GetComponentInParent<HullHealth>();
        maxHealth = weapon.weaponData.maxHealth;
        currentHealth = maxHealth;

        explosionAndSmoke.gameObject.SetActive(false);
    }

    public override bool TakeDamage(float damage)
    {

        bool hitHull = shipHull.TakeDamage((damage / 4) + (currentHealth - damage < 0 ? Mathf.Abs(currentHealth - damage) : 0));

        if (hitHull == true)
        {
            currentHealth -= damage;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                explosionAndSmoke.gameObject.SetActive(true);
                weapon.active = false;
            }
        }

        return hitHull;
    }

    public override void Repair(float repairPoints)
    {
        currentHealth += repairPoints;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
