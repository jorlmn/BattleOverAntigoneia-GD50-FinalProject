using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponsManager : MonoBehaviour
{
    ShipSystemsManager shipSystems = null;
    AIStateManager aiStateManager = null;

    public CriticalPartsManager target;
    public float aiInAcuraccy = 5;

    public Dictionary<Weapon, Transform> weaponTargets = new ();

    public string preferredShipPartTarget = "hull";  

    void Start()
    {
        shipSystems = GetComponent<ShipSystemsManager>();

        foreach(Weapon weapon in shipSystems.weaponsList)
        {
            weaponTargets.Add(weapon, null);
        }


        aiStateManager = GetComponent<AIStateManager>();
    }

    public void StartEngaging()
    {
        StopAllCoroutines();
        StartCoroutine(SetWeaponTarget());
        StartCoroutine(EngageEnemy());
    }

    public void StopEngaging()
    {
        StopAllCoroutines();
        ResetWeapons();
    }

    IEnumerator EngageEnemy()
    {
        while (aiStateManager.aiFightState == aiFightState.engaging)
        {
            foreach(Weapon weapon in shipSystems.weaponsList)
            {
                if (weaponTargets[weapon] != null && weapon.active)
                {
                    weapon.RotateWeapon(weaponTargets[weapon].position);

                    if (weapon.WithinAngleToFire(weaponTargets[weapon].position) && weapon.WithinDistanceToFire(weaponTargets[weapon].position) && weapon.NotHittingOtherAIOrSource(weaponTargets[weapon].position))
                    {
                        weapon.Shoot(weaponTargets[weapon].position, aiInAcuraccy);
                    }
                }
            }

            yield return null;
        }

        yield break;
    }

    IEnumerator SetWeaponTarget()
    {
        while (aiStateManager.aiFightState == aiFightState.engaging)
        {
            foreach(Weapon weapon in shipSystems.weaponsList)
            {
                if (weapon.active)
                {   
                    weaponTargets[weapon] = FindClosestTargetPoint(weapon);
                }
            }

            yield return new WaitForSeconds(4);
        }
        yield break;
    }

    private void ResetWeapons()
    {
        foreach(Weapon weapon in shipSystems.weaponsList)
        {
            weaponTargets[weapon] = null;
        }

        StartCoroutine(WeaponRotationReset());
    }

    IEnumerator WeaponRotationReset()
    {
        float duration = 3f;
        while (duration > 0)
        {
            foreach(Weapon weapon in shipSystems.weaponsList)
            {
                if (weapon.active)
                {   
                    weapon.ResetRotation();
                }
            }
            duration -= Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    private Transform FindClosestTargetPoint(Weapon weapon)
    {
        Transform currentTarget = null;
        float currentDistance = Vector3.Distance(weapon.transform.position, target.criticalParts[preferredShipPartTarget][0].position);
        
        for (int i = 0; i < target.criticalParts[preferredShipPartTarget].Count; i++)
        {
            if (target.criticalParts[preferredShipPartTarget][i].TryGetComponent<Health>(out Health health))
            {
                if (health.currentHealth > 0)
                {
                    if (weapon.WithinFireArc(target.criticalParts[preferredShipPartTarget][i].position))
                    {
                        if (currentDistance >= Vector3.Distance(weapon.transform.position, target.criticalParts[preferredShipPartTarget][i].position))
                        {
                            currentTarget = target.criticalParts[preferredShipPartTarget][i];
                            currentDistance = Vector3.Distance(weapon.transform.position, target.criticalParts[preferredShipPartTarget][i].position);
                        }
                    }
                }
            }
            else
            {
                if (weapon.WithinFireArc(target.criticalParts[preferredShipPartTarget][i].position))
                {
                    if (currentDistance >= Vector3.Distance(weapon.transform.position, target.criticalParts[preferredShipPartTarget][i].position))
                    {
                        currentTarget = target.criticalParts[preferredShipPartTarget][i];
                        currentDistance = Vector3.Distance(weapon.transform.position, target.criticalParts[preferredShipPartTarget][i].position);
                    }
                }
            }
        }
        
        if (currentTarget == null && preferredShipPartTarget != "hull")
        {
            for (int i = 0; i < target.criticalParts["hull"].Count; i++)
            {
                if (weapon.WithinFireArc(target.criticalParts["hull"][i].position))
                {
                    if (currentDistance >= Vector3.Distance(weapon.transform.position, target.criticalParts["hull"][i].position))
                    {
                        currentTarget = target.criticalParts["hull"][i];
                        currentDistance = Vector3.Distance(weapon.transform.position, target.criticalParts["hull"][i].position);
                    }
                }
            }
        }
        return currentTarget;
    }


}
