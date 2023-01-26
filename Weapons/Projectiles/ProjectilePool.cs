using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance = null;
    private Dictionary<int, List<GameObject>>Pools = new();
    [SerializeField] int amountToPool = 50;

    private void Awake()
    {
        if (ProjectilePool.instance == null)
        {
            instance = this;
        }
        else if (ProjectilePool.instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < SOReference.instance.projectileSos.Count; i++)
        {
            if (!Pools.ContainsKey(SOReference.instance.projectileSos[i].id))
            {
                Pools.Add(SOReference.instance.projectileSos[i].id, new List<GameObject>());

                for (int z = 0; z < amountToPool; z++)
                {
                    GameObject projectile = Instantiate(SOReference.instance.projectileSos[i].projectilePrefab, instance.transform);
                    projectile.SetActive(false);
                    Pools[SOReference.instance.projectileSos[i].id].Add(projectile);
                }
            }
        }
    }

    public GameObject GetProjectilePrefab(int projectileID)
    {
        int activeBullets = 0;

        for (int i = 0; i < Pools[projectileID].Count; i++)
        {
            if (Pools[projectileID][i].activeInHierarchy)
            {
                activeBullets++;
            }

            if (!Pools[projectileID][i].activeInHierarchy)
            {
                return Pools[projectileID][i];
            }
        }

        if (activeBullets == Pools[projectileID].Count)
        {                                                     
            GameObject additionalProjectile = Instantiate(Pools[projectileID][0], instance.transform);
            additionalProjectile.SetActive(false);
            Pools[projectileID].Add(additionalProjectile);
            return additionalProjectile;
        }
        return null;
    }
}
