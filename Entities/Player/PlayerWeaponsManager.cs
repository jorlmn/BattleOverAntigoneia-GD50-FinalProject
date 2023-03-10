using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    PlayerTarget target = null;
    ShipSystemsManager shipSystems = null;
    public List<Weapon> selectedWeapons = new();

    [SerializeField] float playerInAccuracy;
    [SerializeField] float playerDamageModifier;

    void Start()
    {
        target = GetComponent<PlayerTarget>();

        shipSystems = GetComponent<ShipSystemsManager>();

        SelectWeapon(1);
    }

    void Update()
    {
        if (StateManager.AimState == StateManager.AimStates.aiming)
        {
            foreach(Weapon weapon in selectedWeapons)
            {
                if (weapon.active && weapon.WithinFireArc(target.aimPoint))
                {
                    weapon.RotateWeapon(target.aimPoint);
                }
            }
        }
    }

    public void SelectWeapon(int weaponIndex)
    {
        selectedWeapons.Clear();

        if (shipSystems.weaponsByType.ContainsKey(weaponIndex))
        {
            foreach(Weapon weapon in shipSystems.weaponsByType[weaponIndex])
            {
                selectedWeapons.Add(weapon);
            }
        }
    }

    public void FireWeapon()
    {
        foreach(Weapon weapon in selectedWeapons)
        {
            if (weapon.active)
            {
                if (weapon.WithinAngleToFire(target.aimPoint) && weapon.WithinDistanceToFire(target.aimPoint) && weapon.NotHittingSource(target.aimPoint))
                {
                    weapon.Shoot(target.aimPoint, playerInAccuracy);
                }
            }
        }
    }
}
