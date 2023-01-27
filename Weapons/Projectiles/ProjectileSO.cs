using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ProjectileSO",menuName ="ProjectileSO/Create New ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
 [Header("Projectile Data")]
    public int id;
    public int damage;
    public int speed;
    public float lifeCountdown = 4f;
    public GameObject projectilePrefab;

    [Header("Gun Data")]
    public float defaultInAccuracy;
    public float fireAgainDelay;
    public float gunMaxRange;
    public float gunMinRange;

    public int angleToFire = 10;
    public LayerMask isDamageable;

    public int standardDamageParticle = 0;
    public int shieldParticle = 0;
    public int criticalDamageParticle = 0;
}
