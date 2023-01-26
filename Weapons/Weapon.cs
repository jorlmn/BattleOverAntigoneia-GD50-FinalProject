using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Projectile")]
    public ProjectileSO projectileData;
    public int projectileQuantity;

    [Header("Firing")]
    public bool justFired = false;
    public float firingCoolDown;
    public bool justReloaded = false;
    public float reloadCoolDown;

    [Header("Turret Data")]
    public TurretDataSO turretData;

    public Transform mainFirePosition;

    public Transform sideWaysPivot;
    public Transform upAndDownPivot;

    public abstract void Shoot(Vector3 aimPoint);
}
