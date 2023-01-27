using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponDataSO", menuName = "WeaponDataSO/Create New WeaponDataSO")]
public class WeaponDataSO : ScriptableObject
{
    public bool is360Turret = false;
    public float rotationSpeed = 300f;
    public float upwardsAngleLimit;
    public float downwardsAngleLimit;
    public float sidewaysAngleLimit;
    public int weaponType = 1;
    public float maxHealth;
}
