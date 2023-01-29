using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStart : MonoBehaviour
{
    
    [SerializeField] GameObject fighter;

    [SerializeField] GameObject cruiser;

    [SerializeField] GameObject battleship;


    [SerializeField] CameraManager cameraManager;

    void Awake()
    {
        Time.timeScale = 1;
        StartCoroutine(StartCoolDown());
    }

    void Start()
    {
        switch (MatchSettings.shipChosenIndex)
        {
            case 0:
                InputManager.instance.playerMovement = fighter.GetComponent<PlayerMovement>();
                InputManager.instance.playerWeapons = fighter.GetComponent<PlayerWeaponsManager>();
                cameraManager.playerShipTarget = fighter.GetComponent<ShipSystemsManager>().cameraTarget;
                break;
            case 1:

                InputManager.instance.playerMovement = cruiser.GetComponent<PlayerMovement>();
                InputManager.instance.playerWeapons = cruiser.GetComponent<PlayerWeaponsManager>();
                cameraManager.playerShipTarget = cruiser.GetComponent<ShipSystemsManager>().cameraTarget;
                break;
            case 2:

                InputManager.instance.playerMovement = battleship.GetComponent<PlayerMovement>();
                InputManager.instance.playerWeapons = battleship.GetComponent<PlayerWeaponsManager>();
                cameraManager.playerShipTarget = battleship.GetComponent<ShipSystemsManager>().cameraTarget;
                break;
        }
    }

    IEnumerator StartCoolDown()
    {
        StateManager.GameState = StateManager.gameStates.cutsceneState;
        yield return new WaitForSecondsRealtime(1);

        switch (MatchSettings.shipChosenIndex)
        {
            case 0:
                fighter.SetActive(true);
                break;
            case 1:
                cruiser.SetActive(true);
                break;
                            case 2:
                battleship.SetActive(true);
                break;
        }

        StateManager.GameState = StateManager.gameStates.playState;
        Time.timeScale = 1;
        yield break;
    }
}
