using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipSystemsManager : MonoBehaviour
{
    public ShipDataSO shipData;
    public Weapon[] weaponsList;
    public Dictionary<int, List<Weapon>> weaponsByType = new();

    [HideInInspector] public HullHealth shipHealth;
    [HideInInspector] public ShipEngine shipEngine;
    [HideInInspector] public ShipShield shipShield;

    public Transform cameraTarget;

    public UnityEvent OnShipDeath = new UnityEvent();
    void Awake()
    {
        shipHealth = GetComponent<HullHealth>();
        shipHealth.maxHealth = shipData.maxHullHealth;
        shipHealth.currentHealth = shipData.maxHullHealth;

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

        OnShipDeath.AddListener(SpawnShipWreck);
    }

    public void TriggerShipDeath()
    {
        OnShipDeath.Invoke();
    }

    private void SpawnShipWreck()
    {
        GameObject shipWreck = Instantiate(shipData.shipWreck);
        shipWreck.gameObject.SetActive(false);
        shipWreck.transform.SetPositionAndRotation(transform.position, transform.rotation);

        shipWreck.SetActive(true);
        gameObject.SetActive(false);
    }
}
