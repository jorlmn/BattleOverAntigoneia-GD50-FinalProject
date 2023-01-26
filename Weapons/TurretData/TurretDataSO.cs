using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TurretDataSO", menuName = "TurretDataSO/Create New TurretDataSO")]
public class TurretDataSO : ScriptableObject
{
    public bool is360Turret = false;
    public float rotationSpeed = 300f;
    public float upwardsAngleLimit;
    public float downwardsAngleLimit;
    public float sidewaysAngleLimit;
    public int turretType = 1;
    public bool active = true;
}
