using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponExtensions
{
    public static void RotateWeapon(this Weapon weapon, Vector3 hitPoint)
    {
        // X rotation

        Quaternion mainXRotation = Quaternion.RotateTowards(weapon.sideWaysPivot.rotation, Quaternion.LookRotation(hitPoint - weapon.sideWaysPivot.position), weapon.weaponData.rotationSpeed * Time.deltaTime);
        Vector3 sideWaysEulerAngles = mainXRotation.eulerAngles;

        sideWaysEulerAngles.x = weapon.sideWaysPivot.rotation.x;
        sideWaysEulerAngles.z = weapon.sideWaysPivot.rotation.z;

        if (!weapon.weaponData.is360Turret)
        {
            sideWaysEulerAngles.y = (sideWaysEulerAngles.y > 180) ? sideWaysEulerAngles.y - 360 : sideWaysEulerAngles.y;

            weapon.sideWaysPivot.rotation = Quaternion.Euler(sideWaysEulerAngles);

            Vector3 newYEulerAngles = weapon.sideWaysPivot.localEulerAngles;
            newYEulerAngles.x = 0;
            newYEulerAngles.z = 0;
            newYEulerAngles.y = (newYEulerAngles.y > 180) ? newYEulerAngles.y - 360 : newYEulerAngles.y;
            newYEulerAngles.y = Mathf.Clamp(newYEulerAngles.y, -weapon.weaponData.sidewaysAngleLimit, weapon.weaponData.sidewaysAngleLimit);
            weapon.sideWaysPivot.localRotation = Quaternion.Euler(newYEulerAngles);
        }
        else
        {
            weapon.sideWaysPivot.rotation = Quaternion.Euler(sideWaysEulerAngles);
        }

        // Y rotation
        Quaternion mainYRotation = Quaternion.RotateTowards(weapon.upAndDownPivot.rotation, Quaternion.LookRotation(hitPoint - weapon.upAndDownPivot.position), weapon.weaponData.rotationSpeed * Time.deltaTime);
        Vector3 upAndDownRotation = mainYRotation.eulerAngles;

        upAndDownRotation.y = weapon.upAndDownPivot.rotation.y;
        upAndDownRotation.z = weapon.upAndDownPivot.rotation.z;

        upAndDownRotation.x = (upAndDownRotation.x > 180) ? upAndDownRotation.x - 360 : upAndDownRotation.x;

        weapon.upAndDownPivot.rotation = Quaternion.Euler(upAndDownRotation);

        Vector3 newXEulerAngles = weapon.upAndDownPivot.localEulerAngles;
        newXEulerAngles.y = 0;
        newXEulerAngles.z = 0;
        newXEulerAngles.x = (newXEulerAngles.x > 180) ? newXEulerAngles.x - 360 : newXEulerAngles.x;
        newXEulerAngles.x = Mathf.Clamp(newXEulerAngles.x, weapon.weaponData.upwardsAngleLimit, weapon.weaponData.downwardsAngleLimit);
        weapon.upAndDownPivot.localRotation = Quaternion.Euler(newXEulerAngles);
    }


    public static bool WithinAngleToFire(this Weapon weapon, Vector3 targetPosition)
    {
        if (Vector3.Angle(targetPosition - weapon.mainFirePosition.position, weapon.mainFirePosition.forward) <= weapon.projectileData.angleToFire && Vector3.Angle(targetPosition - weapon.mainFirePosition.position, weapon.mainFirePosition.forward) >= -weapon.projectileData.angleToFire)
        {
            return true;
        }
        return false;
    }
    public static bool WithinDistanceToFire(this Weapon weapon, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(targetPosition, weapon.mainFirePosition.position);
        if (distance <= weapon.projectileData.gunMaxRange && distance >= weapon.projectileData.gunMinRange)
        {
            return true;
        }

        return false;
    }
}
