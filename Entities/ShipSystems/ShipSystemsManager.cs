using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemsManager : MonoBehaviour
{
    public ShipDataSO shipData;
    public Weapon[] weaponsList;
    public Dictionary<int, List<Weapon>> weaponsByType = new();

    [HideInInspector] public HullHealth shipHealth;
    [HideInInspector] public ShipEngine shipEngine;
    [HideInInspector] public ShipShield shipShield;

    public Transform cameraTarget;
    void Awake()
    {
        shipHealth = GetComponent<HullHealth>();
        shipHealth.maxHullHealth = shipData.maxHullHealth;
        shipHealth.currentHullHealth = shipData.maxHullHealth;

        shipEngine = GetComponent<ShipEngine>();
        shipEngine.shipData = shipData;
        shipEngine.shipRb = GetComponent<Rigidbody>();

        if (TryGetComponent<ShipShield>(out shipShield))
        {
            shipShield.shipData = shipData;
            
            shipHealth.shipShield = shipShield;
        }
    }
    void Start()
    {
        foreach (Weapon weapon in weaponsList)
        {
            if (!weaponsByType.ContainsKey(weapon.weaponType))
            {
                weaponsByType.Add(weapon.GetComponent<Weapon>().weaponType, new List<Weapon>());
            }

            weaponsByType[weapon.weaponType].Add(weapon);
        }
    }
}
