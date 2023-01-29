using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryWeapon : Weapon
{
    public override void Shoot(Vector3 aimPoint)
    {
        if (!justFired)
        {
            Vector3 gunSpread = new();
            gunSpread.x = Random.Range(-(projectileData.defaultInAccuracy), projectileData.defaultInAccuracy);
            gunSpread.y = Random.Range(-(projectileData.defaultInAccuracy), projectileData.defaultInAccuracy);
            gunSpread.z = 0;

            GameObject bullet = ProjectilePool.instance.GetProjectilePrefab(projectileData.id);
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(mainFirePosition.position, mainFirePosition.rotation);
                Projectile projectileScript = bullet.GetComponent<Projectile>();
                projectileScript.projectileData = projectileData;
                projectileScript.timeToRemove = projectileData.lifeCountdown;

                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = transform.GetComponentInParent<Rigidbody>().velocity + (aimPoint - mainFirePosition.position).normalized * projectileData.speed + transform.TransformVector(gunSpread);

            }

            muzzleFlash.gameObject.SetActive(false);
            muzzleFlash.gameObject.SetActive(true);
            
            justFired = true;
            firingCoolDown = projectileData.fireAgainDelay;
            StartCoroutine(ResetFiringCoolDown());
        }
        else if (justFired)
        {
            //Debug.Log("The Weapon is cooling down");
        }
        else if (!justFired)
        {
            //Debug.Log("The weapon is reloading");
        }
        else
        {
            //Debug.Log("The clip is empty");
        }
    }

    IEnumerator ResetFiringCoolDown()
    {
        while (firingCoolDown > 0)
        {
            yield return new WaitForEndOfFrame();

            firingCoolDown -= 1 * Time.deltaTime;

            if (firingCoolDown <= 0)
            {
                justFired = false;
                firingCoolDown = projectileData.fireAgainDelay;
                yield break;
            }
        }
    }
}