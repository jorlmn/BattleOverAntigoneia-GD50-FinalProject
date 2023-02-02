using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : Weapon
{
    private LineRenderer beamRenderer;
    [SerializeField] LayerMask defaultLayer;

    void Start()
    {
        beamRenderer = GetComponent<LineRenderer>();
        beamRenderer.enabled = false;
    }
    public override void Shoot(Vector3 aimPoint, float inaccuracy = 0, float extraReloadTime = 0, float damageModifier = 1)
    {
        if (!justFired)
        {
            StartCoroutine(ActivateBeam(damageModifier));

            muzzleFlash.gameObject.SetActive(false);
            muzzleFlash.gameObject.SetActive(true);
            
            justFired = true;
            firingCoolDown = projectileData.fireAgainDelay + extraReloadTime;
            StartCoroutine(ResetFiringCoolDown());
        }
    }

    IEnumerator ActivateBeam(float damageModifier)
    {
        beamRenderer.enabled = true;

        float beamTimer = projectileData.lifeCountdown;

        float halfASecond = 0;
        while (beamTimer > 0)
        {
            halfASecond += Time.deltaTime;
            Vector3 beamTarget = Vector3.zero;
            if (Physics.Raycast(new Ray(mainFirePosition.position, mainFirePosition.forward), out RaycastHit hit, projectileData.gunMaxRange, defaultLayer, QueryTriggerInteraction.Collide))
            {
                beamTarget = hit.point;

                if (halfASecond > 0.5f)
                {
                    if (hit.transform.gameObject.TryGetComponent<Health>(out Health targetHealth))
                    {
                        bool hitHull = targetHealth.TakeDamage(Random.Range(projectileData.minDamage, projectileData.maxDamage) * damageModifier);

                        if (hitHull == true)
                        {
                            ParticleSystem explosionParticle = ParticlePool.instance.GetParticleSystem(projectileData.standardDamageParticleIndex);
                            explosionParticle.transform.position = hit.point;
                            explosionParticle.gameObject.SetActive(true);
                        }
                        else
                        {
                            ParticleSystem shieldParticle = ParticlePool.instance.GetParticleSystem(projectileData.shieldParticleIndex);
                            shieldParticle.transform.position = hit.point;
                            shieldParticle.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        ParticleSystem explosionParticle = ParticlePool.instance.GetParticleSystem(projectileData.standardDamageParticleIndex);
                        explosionParticle.transform.position = hit.point;
                        explosionParticle.gameObject.SetActive(true);
                    }

                    halfASecond = 0;
                }

            }
            else
            {
                beamTarget = mainFirePosition.position + mainFirePosition.forward * projectileData.gunMaxRange;
            }
            beamRenderer.SetPosition(0, mainFirePosition.position);
            beamRenderer.SetPosition(1, beamTarget);

            yield return new WaitForEndOfFrame();

            beamTimer -= 1 * Time.deltaTime;

            if (beamTimer <= 0)
            {
                muzzleFlash.gameObject.SetActive(false);
                beamRenderer.enabled = false;
                yield break;
            }
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
