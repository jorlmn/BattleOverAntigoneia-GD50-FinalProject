using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemsManager : MonoBehaviour
{
    public ShipDataSO shipData;
    [SerializeField] Weapon[] weaponsList;
    public Dictionary<int, List<Weapon>> weaponsByType = new();

    private ShipHullHealth shipHealth;
    void Start()
    {
        shipHealth = GetComponent<ShipHullHealth>();
        shipHealth.maxHullHealth = shipData.maxHealth;
        shipHealth.currentHullHealth = shipData.maxHealth;

        foreach (Weapon weapon in weaponsList)
        {
            if (!weaponsByType.ContainsKey(weapon.weaponData.weaponType))
            {
                weaponsByType.Add(weapon.GetComponent<Weapon>().weaponData.weaponType, new List<Weapon>());
            }

            weaponsByType[weapon.weaponData.weaponType].Add(weapon);
        }
    }
}
