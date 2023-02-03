using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class GameEndManager : MonoBehaviour
{
    public static GameEndManager instance = null;
    private void Awake()
    {
        if (GameEndManager.instance == null)
        {
            instance = this;
        }
        else if (GameEndManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerDeath()
    {
        StateManager.instance.GameStateChange(StateManager.gameStates.gameOverState);
    }

    public void OnPlayerVictory()
    {
        StateManager.instance.GameStateChange(StateManager.gameStates.gameWonState);
    }
}
