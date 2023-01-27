using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public abstract void TakeDamage(float damage);
    public abstract void Repair(float repairPoints);
}
