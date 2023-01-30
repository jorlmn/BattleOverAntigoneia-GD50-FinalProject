using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Projectile")]
    public ProjectileSO projectileData;
    public ParticleSystem muzzleFlash;

    [Header("Firing")]
    public bool justFired = false;
    public float firingCoolDown;
    public Transform source;

    [Header("Weapon Data")]
    public WeaponDataSO weaponData;
    public int weaponType = 1;
    public bool active = true;

    public Transform mainFirePosition;
    public Transform sideWaysPivot;
    public Transform upAndDownPivot;

    void Start()
    {
        source = GetComponentInParent<ShipSystemsManager>().transform;
        muzzleFlash = mainFirePosition.GetComponentInChildren<ParticleSystem>();
        muzzleFlash.gameObject.SetActive(false);
    }
    public abstract void Shoot(Vector3 aimPoint, float inaccuracy = 0, float extraReloadTime = 0);
}
