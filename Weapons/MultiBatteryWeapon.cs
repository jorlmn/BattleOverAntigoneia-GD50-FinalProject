using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBatteryWeapon : Weapon
{
    [SerializeField] List<Transform> extraCannons = new();
    private List<ParticleSystem> extraMuzzleFlashes = new();

    void Start()
    {
        source = GetComponentInParent<ShipSystemsManager>().transform;
        muzzleFlash = mainFirePosition.GetComponentInChildren<ParticleSystem>();
        muzzleFlash.gameObject.SetActive(false);

        for (int i = 0; i < extraCannons.Count; i++)
        {
            extraMuzzleFlashes.Add(extraCannons[i].GetComponentInChildren<ParticleSystem>());
            extraMuzzleFlashes[i].gameObject.SetActive(false);
        }
    }
    public override void Shoot(Vector3 aimPoint, float inaccuracy = 0, float extraReloadTime = 0)
    {
        if (!justFired)
        {
            Vector3 gunSpread = new();
            gunSpread.x = Random.Range(-(projectileData.defaultInAccuracy + inaccuracy), projectileData.defaultInAccuracy + inaccuracy);
            gunSpread.y = Random.Range(-(projectileData.defaultInAccuracy + inaccuracy), projectileData.defaultInAccuracy + inaccuracy);
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

            FireExtraCannons(aimPoint, inaccuracy);

            
            justFired = true;
            firingCoolDown = projectileData.fireAgainDelay + extraReloadTime;
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

    private void FireExtraCannons(Vector3 aimPoint, float inaccuracy = 0)
    {
        for (int i = 0; i < extraCannons.Count; i++)
        {
            Vector3 gunSpread = new();
            gunSpread.x = Random.Range(-(projectileData.defaultInAccuracy + inaccuracy), projectileData.defaultInAccuracy + inaccuracy);
            gunSpread.y = Random.Range(-(projectileData.defaultInAccuracy + inaccuracy), projectileData.defaultInAccuracy + inaccuracy);
            gunSpread.z = 0;

            GameObject bullet = ProjectilePool.instance.GetProjectilePrefab(projectileData.id);
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(extraCannons[i].position, extraCannons[i].rotation);
                Projectile projectileScript = bullet.GetComponent<Projectile>();
                projectileScript.projectileData = projectileData;
                projectileScript.timeToRemove = projectileData.lifeCountdown;

                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = transform.GetComponentInParent<Rigidbody>().velocity + (aimPoint - extraCannons[i].position).normalized * projectileData.speed + transform.TransformVector(gunSpread);

            }

            extraMuzzleFlashes[i].gameObject.SetActive(false);
            extraMuzzleFlashes[i].gameObject.SetActive(true);
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
