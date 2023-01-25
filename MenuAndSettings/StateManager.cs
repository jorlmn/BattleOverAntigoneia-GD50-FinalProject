using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance = null;

    [Header("Objects")]
    [SerializeField]
    Animator matchMenu;

    [SerializeField]
    AudioSource mainMenuAudio;

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

    void Start()
    {
        GameState = gameStates.playState;
        Time.timeScale = 1;
    }

    public enum gameStates
    {
        playState,
        pauseState,
        cutsceneState,
        panelInteractionState
    }

    public enum AimStates
    {
        aiming,
        notAiming
    }

    public static gameStates GameState = gameStates.playState;

    public static AimStates AimState = AimStates.notAiming;

    public void GameStateChange(gameStates state)
    {
        GameState = state;

        GameStateEntry();
    }

    public void AimStateChange(AimStates state)
    {
        AimState = state;

        AimStateEntry();
    }

    private void GameStateEntry()
    {
        switch (GameState)
        {
            case gameStates.playState:
                matchMenu.SetTrigger("CloseMenu");
                mainMenuAudio.Play();

                Time.timeScale = 1;
                break;
            case gameStates.pauseState:
                matchMenu.SetTrigger("OpenMenu");
                mainMenuAudio.Play();

                AimStateChange(AimStates.notAiming);
                Time.timeScale = 0;
                break;
        }

        cursorComponent.UpdateCursor();
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
