using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public abstract bool TakeDamage(float damage);
    public abstract void Repair(float repairPoints);
}
