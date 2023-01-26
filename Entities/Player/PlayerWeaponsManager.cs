using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    PlayerTarget target = null;
    ShipSystemsManager shipSystems = null;
    public int selectedWeaponIndex = 1;
    public List<Weapon> selectedWeapons = new();

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
                weapon.RotateWeapon(target.aimPoint);
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
                if (weapon.turretData.active)
                {
                    selectedWeapons.Add(weapon);
                }
            }
        }
    }

    public void FireWeapon()
    {
        foreach(Weapon weapon in selectedWeapons)
        {
            if (weapon.WithinAngleToFire(target.aimPoint) && weapon.WithinDistanceToFire(target.aimPoint))
            {
                weapon.Shoot(target.aimPoint);
            }
        }
    }
}
