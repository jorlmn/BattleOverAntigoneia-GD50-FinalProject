using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStart : MonoBehaviour
{
    
    [SerializeField] GameObject scout;

    [SerializeField] GameObject raider;

    [SerializeField] GameObject freighter;

    [SerializeField] GameObject cruiser;

    [SerializeField] GameObject battleship;


    [SerializeField] CameraManager cameraManager;

    private GameObject selectedShip = null;

    [SerializeField] List<GameObject> aiShips = new();

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
                selectedShip = scout;
                break;
            case 1:
                selectedShip = raider;
                break;
            case 2:
                selectedShip = freighter;
                break;
            case 3:
                selectedShip = cruiser;
                break;
            case 4:
                selectedShip = battleship;

                cameraManager.maxZoomDistance = 80f;
                cameraManager.defaultIdleZoomDistance = 30f;
                cameraManager.aimingMinZoomDistance = 30f;
                break;
        }

        InputManager.instance.playerMovement = selectedShip.GetComponent<PlayerMovement>();
        InputManager.instance.playerWeapons = selectedShip.GetComponent<PlayerWeaponsManager>();
        selectedShip.GetComponent<PlayerTarget>().playerCamera = cameraManager.GetComponent<Camera>();
        cameraManager.playerShipTarget = selectedShip.GetComponent<ShipSystemsManager>().cameraTarget;

        foreach(AIDataSO aiData in SOReference.instance.difficultyPresets)
        {
            if (!MatchSettings.difficultyPresets.ContainsKey(aiData.preset))
            {
                MatchSettings.difficultyPresets.Add(aiData.preset, aiData);
            }
        }
    }

    IEnumerator StartCoolDown()
    {
        StateManager.GameState = StateManager.gameStates.cutsceneState;
        yield return new WaitForSecondsRealtime(1);


        selectedShip.SetActive(true);

        selectedShip.GetComponent<ShipSystemsManager>().OnDeath += GameEndManager.instance.OnPlayerDeath;

        foreach (GameObject aiShip in aiShips)
        {
            aiShip.GetComponent<AIWeaponsManager>().target = selectedShip.GetComponent<CriticalPartsManager>();
            aiShip.SetActive(true);
        }

        StateManager.GameState = StateManager.gameStates.playState;
        Time.timeScale = 1;
        yield break;
    }
}
