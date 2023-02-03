using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{

    [Header("Cursor Sprites")]
    [SerializeField] Sprite AimCrossHair;
    [SerializeField] Sprite PanelCursor;

    [Header("Cursor Object")]
    [SerializeField] GameObject customCursor;
    private Image cursorImage;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cursorImage = customCursor.GetComponent<Image>();
        cursorImage.sprite = AimCrossHair;
    }

    void OnApplicationFocus()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    void Update()
    {
        customCursor.transform.position = Input.mousePosition;
    }

    public void UpdateCursor()
    {
        switch (StateManager.GameState)
        {
            case StateManager.gameStates.playState:

                if (StateManager.AimState == StateManager.AimStates.aiming)
                {
                    cursorImage.sprite = AimCrossHair;
                    Cursor.lockState = CursorLockMode.Confined;
                    customCursor.SetActive(true);
                }
                else
                {
                    customCursor.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;

            case StateManager.gameStates.cutsceneState:
                customCursor.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                break;

            default:
                cursorImage.sprite = PanelCursor;
                customCursor.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                break;
        }
    }



}
