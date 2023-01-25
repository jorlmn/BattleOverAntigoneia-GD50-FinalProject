using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemsManager : MonoBehaviour
{
    public ShipDataSO shipData;
    public float currentHealth;
    public GameObject[] primaryWeapons;

    void Start()
    {
        currentHealth = shipData.maxHealth;
    }
}
