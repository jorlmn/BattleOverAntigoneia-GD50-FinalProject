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
        bulletTrail.emitting = false;
        previousPosition = transform.position;
    }


    private void FixedUpdate()
    {
        if (Physics.Raycast(new Ray(previousPosition, (transform.position - previousPosition).normalized), out RaycastHit hit, (transform.position - previousPosition).magnitude, projectileData.isDamageable, QueryTriggerInteraction.Collide))
        {
        
            //se o projetil acertou diretamente um objeto que tem o componente de parte do corpo
            // if (hit.transform.gameObject.TryGetComponent<ShipPart>(out ShipPart shipPart))
            // {

            //     ShipSystemsManager shipSystem = shipPart.entityHealthComponent;
            //     if (!shipSystem.entityHealthData.isArmoured)
            //     {
            //         //personagem recebe o dano, multiplicado pelo damageMultiplier daquela parte específica do corpo
            //         entityHealth.TakeDamage(ammoData.ammoDamage * entityBodyPart.bodyOrVehiclePart.damageMultiplier);
            //     }
            //     else if (entityHealth.entityHealthData.isArmoured)
            //     {
            //         //se o objeto era blindado, mas o projeto era anti tank, objeto recebe o dano anti tank do projetil
            //         if (ammoData.isAntiTank)
            //         {
            //             entityHealth.TakeDamage(ammoData.antiTankDamage);
            //         }
            //     }
            // }

            //desativa o projetil
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

            if (timeToRemove < projectileData.lifeCountdown - 0.05f && bulletTrail.emitting == false)
            {
                bulletTrail.emitting = true;
            }
        }
    }
}
