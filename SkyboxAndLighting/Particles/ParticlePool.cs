using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool instance = null;
    private Dictionary<int, List<ParticleSystem>>Pools = new ();
    [SerializeField] int amountToPool = 50;

    private void Awake()
    {
        if (ParticlePool.instance == null)
        {
            instance = this;
        }
        else if (ParticlePool.instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < SOReference.instance.particleSos.Count; i++)
        {
            if (!Pools.ContainsKey(SOReference.instance.particleSos[i].id))
            {
                Pools.Add(SOReference.instance.particleSos[i].id, new List<ParticleSystem>());

                for (int z = 0; z < amountToPool; z++)
                {
                    ParticleSystem particle = Instantiate(SOReference.instance.particleSos[i].particle, instance.transform);
                    particle.gameObject.SetActive(false);
                    Pools[SOReference.instance.projectileSos[i].id].Add(particle);
                }
            }
        }
    }

    public ParticleSystem GetParticleSystem(int particleID)
    {
        int activeParticles = 0;

        for (int i = 0; i < Pools[particleID].Count; i++)
        {
            if (Pools[particleID][i].gameObject.activeInHierarchy)
            {
                activeParticles++;
            }

            if (!Pools[particleID][i].gameObject.activeInHierarchy)
            {
                return Pools[particleID][i];
            }
        }

        if (activeParticles == Pools[particleID].Count)
        {                                                     
            ParticleSystem additionalParticle = Instantiate(Pools[particleID][0], instance.transform);
            additionalParticle.gameObject.SetActive(false);
            Pools[particleID].Add(additionalParticle);
            return additionalParticle;
        }
        return null;
    }
}
