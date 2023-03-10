using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    private AIWeaponsManager aiWeapons;

    public aiMovementState aiMovementState = aiMovementState.idle;
    public aiFightState aiFightState = aiFightState.idle;

    void Start()
    {
        aiWeapons = GetComponent<AIWeaponsManager>();
    }

    public void AiMovementStateChange(aiMovementState state)
    {
        aiMovementState = state;

        AiMovementStateEntry();
    }

    public void AiFightStateChange(aiFightState state)
    {
        aiFightState = state;

        AiFightStateEntry();
    }

    private void AiMovementStateEntry()
    {
        //switch (aiMovementState)
        //{

        //}
    }

    private void AiFightStateEntry()
    {
        switch (aiFightState)
        {
            case aiFightState.engaging:
                aiWeapons.StartEngaging();
                break;

            case aiFightState.idle:
                aiWeapons.StopEngaging();
                break;
        }
    }
}

public enum aiMovementState
{
    idle,
    fleeing,
    patrolling,
    chasing
}

public enum aiFightState
{
    idle,
    engaging
}

