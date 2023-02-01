using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AIDataSO",menuName ="AIDataSO/Create New AIDataSO")]
public class AIDataSO : ScriptableObject
{
    public float minFiringDelay;
    public float maxFiringDelay;
    public float minInaccuracy;
    public float maxInaccuracy;

    public float aiDamageModifier;
}
