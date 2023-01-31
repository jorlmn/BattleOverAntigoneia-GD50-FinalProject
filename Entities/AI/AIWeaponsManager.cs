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

    public string shipPart = "hull";  

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
        StartCoroutine(SetWeaponTarget());
        StartCoroutine(EngageEnemy());
    }

    public void StopEngaging()
    {
        StopAllCoroutines();
    }

    IEnumerator EngageEnemy()
    {
        while (aiStateManager.aiFightState == aiFightState.engaging)
        {
            foreach(Weapon weapon in shipSystems.weaponsList)
            {
                if (weaponTargets[weapon] != null)
                {
                    weapon.RotateWeapon(target.transform.position);

                    if (weapon.WithinAngleToFire(target.transform.position) && weapon.WithinDistanceToFire(target.transform.position) && weapon.NotHittingSource(target.transform.position))
                    {
                        weapon.Shoot(target.transform.position, aiInAcuraccy);
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
            Debug.Log("finding new weapon target");
            foreach(Weapon weapon in shipSystems.weaponsList)
            {
                if (weapon.active)
                {   
                    if (weaponTargets[weapon] == null)
                    {
                        weaponTargets[weapon] = FindClosestTargetPoint(weapon);
                    }
                
                    if (weaponTargets[weapon] != null)
                    {
                        if (!weapon.WithinDistanceToFire(weaponTargets[weapon].position))
                        {
                            weaponTargets[weapon] = FindClosestTargetPoint(weapon);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(10);
        }
        yield break;
    }

    private Transform FindClosestTargetPoint(Weapon weapon)
    {
        Transform currentTarget = target.criticalParts[shipPart][0];
        float currentDistance = Vector3.Distance(weapon.transform.position, target.criticalParts[shipPart][0].position);
        
        for (int i = 0; i < target.criticalParts[shipPart].Count; i++)
        {
            if (weapon.WithinFireArc(target.criticalParts[shipPart][i].position))
            {
                if (currentDistance > Vector3.Distance(weapon.transform.position, target.criticalParts[shipPart][i].position))
                {
                    currentTarget = target.criticalParts[shipPart][i];
                    currentDistance = Vector3.Distance(weapon.transform.position, target.criticalParts[shipPart][i].position);
                }
            }
        }

        return currentTarget;
    }


}
