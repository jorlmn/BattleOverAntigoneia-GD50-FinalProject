using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask cameraToCrossHairLayerMask;

    public Vector3 aimPoint = Vector3.zero;

    [SerializeField] Weapon testWeapon;

    void Update()
    {
        if (StateManager.AimState == StateManager.AimStates.aiming)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cameraToCrossHairLayerMask))
            {
                aimPoint = hit.point;
            }

            if (Input.GetKey(Keybindings.keyFireWeapon))
            {
                testWeapon.Shoot(aimPoint);
            }
        }
    }

}
