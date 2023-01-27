using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ProjectileSO",menuName ="ProjectileSO/Create New ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
 [Header("Projectile Data")]
    public int id;
    public int numberOfRounds;
    public int damage;
    public int speed;
    public float lifeCountdown = 10f;
    public GameObject projectilePrefab;

    [Header("Gun Data")]
    public float defaultInAccuracy;
    public float fireAgainDelay;
    public float gunMaxRange;
    public float gunMinRange;

    public int angleToFire = 10;
    public LayerMask isDamageable;
}