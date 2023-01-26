using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemsManager : MonoBehaviour
{
    public ShipDataSO shipData;
    public float currentHealth;
    [SerializeField] Weapon[] weaponsList;
    public Dictionary<int, List<Weapon>> weaponsByType = new();

    void Start()
    {
        currentHealth = shipData.maxHealth;

        foreach (Weapon weapon in weaponsList)
        {
            if (!weaponsByType.ContainsKey(weapon.turretData.turretType))
            {
                weaponsByType.Add(weapon.GetComponent<Weapon>().turretData.turretType, new List<Weapon>());
            }

            weaponsByType[weapon.turretData.turretType].Add(weapon);
        }
    }
}
