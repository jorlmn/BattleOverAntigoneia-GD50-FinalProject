using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalPartsManager : MonoBehaviour
{
    [SerializeField] ShipSystemsManager shipSystems;

    [SerializeField] Transform criticalPartTransform;
    public Dictionary<string, List<Transform>> criticalParts = new();

    void Start()
    {
        criticalParts.Add("weapons", new List<Transform>());

        foreach (Weapon weapon in shipSystems.weaponsList)
        {
            criticalParts["weapons"].Add(weapon.transform);
        }

        criticalParts.Add("engine", new List<Transform>());

        criticalParts["engine"].Add(GetComponentInChildren<EngineHealth>().transform);
        

        criticalParts.Add("hull", new List<Transform>());

        criticalParts["hull"].Add(criticalPartTransform);

        foreach(Transform children in criticalPartTransform)
        {
            criticalParts["hull"].Add(children);
        }
    } 

}
