using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance = null;

    [Header("Objects")]
    [SerializeField] Animator matchMenu;

    [SerializeField] Animator matchEndMenu;

    [SerializeField] AudioSource mainMenuAudio;

    [Header("Components")]
    public CameraManager cameraComponent;
    public CursorManager cursorComponent;


    void Awake()
    {
        if (StateManager.instance == null)
        {
            instance = this;
        }
        else if (StateManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

    public enum gameStates
    {
        playState,
        pauseState,
        cutsceneState,
        gameWonState,
        gameOverState
    }

    public enum AimStates
    {
        aiming,
        notAiming
    }

    public static gameStates GameState = gameStates.cutsceneState;

    public static AimStates AimState = AimStates.notAiming;

    public void GameStateChange(gameStates enteringState)
    {
        GameStateExit(GameState);

        GameStateEntry(enteringState);
    }

    public void AimStateChange(AimStates state)
    {
        AimState = state;

        AimStateEntry();
    }

    void GameStateEntry(gameStates enteringState)
    {
        GameState = enteringState;
        cursorComponent.UpdateCursor();

        switch (GameState)
        {
            case gameStates.playState:
                Time.timeScale = 1;
                break;
            case gameStates.pauseState:
                matchMenu.SetTrigger("OpenMenu");
                mainMenuAudio.Play();
                Time.timeScale = 0;
                break;

            case gameStates.gameOverState:
                matchEndMenu.SetTrigger("GameOver");
                mainMenuAudio.Play();
                InputManager.horizontalInput = 0;
                InputManager.verticalInput = 0;
                break;

            case gameStates.gameWonState:
                matchEndMenu.SetTrigger("GameWon");
                mainMenuAudio.Play();
                InputManager.horizontalInput = 0;
                InputManager.verticalInput = 0;
                break;
        }
    }

    void GameStateExit(gameStates exitingState)
    {
        switch (exitingState)
        {
            case gameStates.playState:
                AimStateChange(AimStates.notAiming);
                break;

            case gameStates.pauseState:
                matchMenu.SetTrigger("CloseMenu");
                mainMenuAudio.Play();
                break;

            case gameStates.gameOverState:
                break;

            case gameStates.gameWonState:
                break;
        }
    }

    private void AimStateEntry()
    {
        switch (AimState)
        {
            case AimStates.aiming:
                cameraComponent.StartedAiming();
                break;
            case AimStates.notAiming:
                cameraComponent.StoppedAiming();
                break;
        }

        cursorComponent.UpdateCursor();
    }


}
