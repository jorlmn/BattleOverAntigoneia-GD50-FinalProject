using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ParticleSO",menuName ="ParticleSO/Create New ParticleSO")]
public class ParticleSO : ScriptableObject
{
    public int id;
    public ParticleSystem particle;
}
