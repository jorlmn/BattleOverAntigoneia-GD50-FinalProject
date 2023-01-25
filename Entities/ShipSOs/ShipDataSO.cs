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
    public int maxHealth;

    public int maxRearSpeed;
    public int standardVelocity;
    public int turboVelocity;
    public float stealthVelocity;
    public int rotationSpeed;
}
