using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keybindings : MonoBehaviour
{
    public static Keybindings instance = null;

    void Awake()
    {
        if (Keybindings.instance == null)
        {
            instance = this;
        }
        else if (Keybindings.instance != this)
        {
            Destroy(gameObject);
        }
    }

    //Keybindings
    public static KeyCode keyCamReset = KeyCode.Minus;
    [SerializeField]
    TextMeshProUGUI textKeyCamReset;
    public static KeyCode keyInteract = KeyCode.E;
    [SerializeField]
    TextMeshProUGUI textKeyInteract;
    public static KeyCode keyReloadWeapon = KeyCode.R;
    [SerializeField]
    TextMeshProUGUI textKeyReload;
    public static KeyCode keyOpenInventory = KeyCode.I;
    [SerializeField]
    TextMeshProUGUI textKeyInventory;
    public static KeyCode keyMoveForward = KeyCode.W;
    [SerializeField]
    TextMeshProUGUI textkeyForward;
    public static KeyCode keyMoveBackward = KeyCode.S;
    [SerializeField]
    TextMeshProUGUI textKeyBackward;
    public static KeyCode keyMoveLeft = KeyCode.A;
    [SerializeField]
    TextMeshProUGUI textKeyleft;
    public static KeyCode keyMoveRight = KeyCode.D;
    [SerializeField]
    TextMeshProUGUI textKeyRight;
    public static KeyCode keyToggleStealth = KeyCode.C;
    [SerializeField]
    TextMeshProUGUI textKeyStealth;
    public static KeyCode keyToggleTurbo = KeyCode.LeftShift;
    [SerializeField]
    TextMeshProUGUI textKeyTurbo;

    public static KeyCode keyFireWeapon = KeyCode.Mouse0;
    public static KeyCode keyAim = KeyCode.Mouse1;
    public static KeyCode keyCamPivot = KeyCode.Mouse2;
    public static KeyCode keyZoomInCamera = KeyCode.KeypadPlus;
    public static KeyCode keyZoomOutCamera = KeyCode.KeypadMinus;
    public static KeyCode keyQuickSlot1 = KeyCode.Alpha1;
    public static KeyCode keyQuickSlot2 = KeyCode.Alpha2;
    public static KeyCode keyQuickSlot3 = KeyCode.Alpha3;
    public static KeyCode keyQuickSlot4 = KeyCode.Alpha4;
    public static KeyCode keyQuickSlot5 = KeyCode.Alpha5;
    public static KeyCode keyQuickSlot6 = KeyCode.Alpha6;
    public static KeyCode keyQuickSlot7 = KeyCode.Alpha7;
    public static KeyCode keyQuickSlot8 = KeyCode.Alpha8;
    public static KeyCode keyOpenEscMenu = KeyCode.Escape;

    public void ResetKeyBindingsToDefault()
    {
        keyCamReset = KeyCode.Minus;
        keyInteract = KeyCode.E;
        keyReloadWeapon = KeyCode.R;
        keyOpenInventory = KeyCode.I;
        keyMoveForward = KeyCode.W;
        keyMoveBackward = KeyCode.S;
        keyMoveLeft = KeyCode.A;
        keyMoveRight = KeyCode.D;
        keyToggleStealth = KeyCode.C;
        keyToggleTurbo = KeyCode.LeftShift;

        
        keyFireWeapon = KeyCode.Mouse0;
        keyAim = KeyCode.Mouse1;
        keyCamPivot = KeyCode.Mouse2;
        keyZoomInCamera = KeyCode.KeypadPlus;
        keyZoomOutCamera = KeyCode.KeypadMinus;
        keyQuickSlot1 = KeyCode.Alpha1;
        keyQuickSlot2 = KeyCode.Alpha2;
        keyQuickSlot3 = KeyCode.Alpha3;
        keyQuickSlot4 = KeyCode.Alpha4;
        keyQuickSlot5 = KeyCode.Alpha5;
        keyQuickSlot6 = KeyCode.Alpha6;
        keyQuickSlot7 = KeyCode.Alpha7;
        keyQuickSlot8 = KeyCode.Alpha8;
        keyOpenEscMenu = KeyCode.Escape;

        PortrayKeybindings();
    }


    public void ApplyNewKeyBindings()
    {
        keyMoveForward = (KeyCode) System.Enum.Parse(typeof(KeyCode), textkeyForward.text);

        keyMoveBackward = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyBackward.text);

        keyMoveLeft = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyleft.text);

        keyMoveRight = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyRight.text);

        keyToggleTurbo = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyTurbo.text);

        keyToggleStealth = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyStealth.text);

        keyInteract = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyInteract.text);

        keyOpenInventory = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyInventory.text);

        keyReloadWeapon = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyReload.text);

        keyCamReset = (KeyCode) System.Enum.Parse(typeof(KeyCode), textKeyCamReset.text);
    }

    public void PortrayKeybindings()
    {
        textkeyForward.text = keyMoveForward.ToString();
        textKeyBackward.text = keyMoveBackward.ToString();
        textKeyleft.text = keyMoveLeft.ToString();
        textKeyRight.text = keyMoveRight.ToString();
        textKeyReload.text = keyReloadWeapon.ToString();
        textKeyInventory.text = keyOpenInventory.ToString();
        textKeyInteract.text = keyInteract.ToString();
        textKeyTurbo.text = keyToggleTurbo.ToString();
        textKeyStealth.text = keyToggleStealth.ToString();
        textKeyCamReset.text = keyCamReset.ToString();
    }
}