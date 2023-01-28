using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShipDataSO",menuName ="ShipDataSO/Create New ShipDataSO")]
public class ShipDataSO : ScriptableObject
{
    [Header("Ship Data")]
    public int id;
    public string shipModel;
    public GameObject shipPrefab;


    [Header("Ship Stats")]
    public int maxHullHealth;
    public int maxEngineHealth;
    public int maxShield;
    public float shieldRecoveryRate;

    [Header("Engine")]
    public int maxRearSpeed;
    public int standardVelocity;
    public int turboVelocity;
    public int stealthVelocity;
    public int rotationSpeed;
    public float turboRecoveryRate;
    public float turboSpendRate;
}
