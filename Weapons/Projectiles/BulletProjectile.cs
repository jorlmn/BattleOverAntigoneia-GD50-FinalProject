using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    TrailRenderer bulletTrail;

    Vector3 previousPosition;
    private void Awake()
    {
        bulletTrail = gameObject.GetComponentInChildren<TrailRenderer>();
        previousPosition = transform.position;
    }
    void OnEnable()
    {
        bulletTrail.Clear();
        previousPosition = transform.position;
    }


    private void FixedUpdate()
    {
        if (Physics.Raycast(new Ray(previousPosition, (transform.position - previousPosition).normalized), out RaycastHit hit, (transform.position - previousPosition).magnitude, projectileData.isDamageable, QueryTriggerInteraction.Collide))
        {
            if (hit.transform.gameObject.TryGetComponent<Health>(out Health shipPart))
            {
                    shipPart.TakeDamage(projectileData.damage);
            }
            timeToRemove = projectileData.lifeCountdown;
            gameObject.SetActive(false);
        }
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (StateManager.GameState != StateManager.gameStates.pauseState)
        {
            if (timeToRemove > 0)
            {
                timeToRemove -= 1 * Time.deltaTime;
            }
            else if (timeToRemove <= 0)
            {
                if (gameObject.activeInHierarchy)
                {
                    timeToRemove = projectileData.lifeCountdown;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}