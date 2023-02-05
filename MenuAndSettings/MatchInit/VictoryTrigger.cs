using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            GameEndManager.instance.OnPlayerVictory();
        }
    }
}
