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

    public abstract void Shoot(Vector3 aimPoint);
    public static bool WithinAngleToFire(Vector3 targetPosition, Transform firePosition, int angle)
    {
        if (Vector3.Angle(targetPosition - firePosition.position, firePosition.forward) <= angle && Vector3.Angle(targetPosition - firePosition.position, firePosition.forward) >= -angle)
        {
            return true;
        }
        return false;
    }
    public static bool WithinDistanceToFire(Vector3 targetPosition, Transform firePosition, float maxDistance, float minDistance = 0)
    {
        float distance = Vector3.Distance(targetPosition, firePosition.position);
        if (distance <= maxDistance && distance >= minDistance)
        {
            return true;
        }

        return false;
    }
}
