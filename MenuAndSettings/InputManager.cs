using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    void Awake()
    {
        if (InputManager.instance == null)
        {
            instance = this;
        }
        else if (InputManager.instance != this)
        {
            Destroy(gameObject);
        }
    }
    public static float horizontalInput = 0f;
    public static float  verticalInput = 0f;

    [Header("Components")]
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] PlayerWeaponsManager playerWeapons;

    void Start()
    {
        horizontalInput = 0f;
        verticalInput = 0f;
    }

    void Update()
    {
        GeneralInputEvents();

        if (StateManager.GameState == StateManager.gameStates.playState)
        {   
            PlayStateInputEvents();

            if (StateManager.AimState == StateManager.AimStates.aiming)
            {
                AimStateInputEvents();
            }
        }

    }

    void GeneralInputEvents()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (StateManager.GameState == StateManager.gameStates.playState)
            {
                StateManager.instance.GameStateChange(StateManager.gameStates.pauseState);
            }
            else if (StateManager.GameState == StateManager.gameStates.panelInteractionState)
            {
                StateManager.instance.GameStateChange(StateManager.gameStates.playState);
            }
            else if (StateManager.GameState == StateManager.gameStates.pauseState)
            {
                StateManager.instance.GameStateChange(StateManager.gameStates.playState);
            }
        }
    }
    void PlayStateInputEvents()
    {
        UpdateMovementAxisInput();

        if (Input.GetKeyDown(Keybindings.keyAim))
        {
            StateManager.instance.AimStateChange(StateManager.AimStates.aiming);
        }
        else if (Input.GetKeyUp(Keybindings.keyAim))
        {
            StateManager.instance.AimStateChange(StateManager.AimStates.notAiming);
        }

        if (Input.GetKeyDown(Keybindings.keyCamReset))
        {
            StateManager.instance.cameraComponent.ResettingCam();
        }

        if (Input.GetKeyDown(Keybindings.keyToggleTurbo))
        {
            playerMovement.ToggleTurbo();
        }

        if (Input.GetKeyDown(Keybindings.keyToggleStealth))
        {
            playerMovement.ToggleStealth();
        }

        if (Input.GetKeyDown(Keybindings.keyQuickSlot1))
        {
            playerWeapons.SelectWeapon(1);
        }
        else if (Input.GetKeyDown(Keybindings.keyQuickSlot2))
        {
            playerWeapons.SelectWeapon(2);
        }
        else if (Input.GetKeyDown(Keybindings.keyQuickSlot3))
        {
            playerWeapons.SelectWeapon(3);
        }
    }

    void AimStateInputEvents()
    {
        if (Input.GetKeyDown(Keybindings.keyFireWeapon))
        {
            playerWeapons.FireWeapon();
        }
    }

    void UpdateMovementAxisInput()
    {
        if (!Input.GetKey(Keybindings.keyMoveForward) && !Input.GetKey(Keybindings.keyMoveBackward) ||
            Input.GetKey(Keybindings.keyMoveForward) && Input.GetKey(Keybindings.keyMoveBackward))
        {
            verticalInput = 0;
        }
        else if (Input.GetKey(Keybindings.keyMoveForward))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(Keybindings.keyMoveBackward))
        {
            verticalInput = -1;
        }

        if (!Input.GetKey(Keybindings.keyMoveRight) && !Input.GetKey(Keybindings.keyMoveLeft) || 
            Input.GetKey(Keybindings.keyMoveRight) && Input.GetKey(Keybindings.keyMoveLeft))
        {
            horizontalInput = 0;
        }
        else if (Input.GetKey(Keybindings.keyMoveRight))
        {
            horizontalInput = 1;
        }
        else if (Input.GetKey(Keybindings.keyMoveLeft))
        {
            horizontalInput = -1;
        }
    }
}
