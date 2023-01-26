using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile Life")]
    public float timeToRemove;

    [Header("Projectile Stats")]
    public ProjectileSO projectileData;
}
