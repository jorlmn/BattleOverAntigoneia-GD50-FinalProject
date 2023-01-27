using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemsManager : MonoBehaviour
{
    public ShipDataSO shipData;
    [SerializeField] Weapon[] weaponsList;
    public Dictionary<int, List<Weapon>> weaponsByType = new();

    [HideInInspector] public ShipHullHealth shipHealth;
    [HideInInspector] public ShipEngine shipEngine;

    void Awake()
    {
        shipHealth = GetComponent<ShipHullHealth>();
        shipHealth.maxHullHealth = shipData.maxHullHealth;
        shipHealth.currentHullHealth = shipData.maxHullHealth;

        shipEngine = GetComponent<ShipEngine>();
        shipEngine.shipData = shipData;
    }
    void Start()
    {
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
