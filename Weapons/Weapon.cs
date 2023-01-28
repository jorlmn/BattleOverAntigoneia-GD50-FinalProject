using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Projectile")]
    public ProjectileSO projectileData;

    [Header("Firing")]
    public bool justFired = false;
    public float firingCoolDown;
    public Transform source;

    [Header("Weapon Data")]
    public WeaponDataSO weaponData;
    public bool active = true;

    public Transform mainFirePosition;
    public Transform sideWaysPivot;
    public Transform upAndDownPivot;

    public abstract void Shoot(Vector3 aimPoint);
}
